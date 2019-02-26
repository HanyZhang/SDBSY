using SDBSY.Common;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class ClassController : Controller
    {
        public IStudentService stuSvc { get; set; }
        public IDataDictionaryService dataSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        // GET: Class
        [CheckPermission("Class.Index")]
        public ActionResult Index(long id)
        {
            //班级名称
            string className = dataSvc.GetById(id).Value;
            //总人数
            int total = stuSvc.GetByClassId(id).Count();
            //男 女
            int boy = stuSvc.GetByClassId(id).Where(t => t.Gender == false).Count();
            int girl= stuSvc.GetByClassId(id).Where(t => t.Gender == true).Count();
            //下一学年留班
            int liuban = stuSvc.GetByClassId(id).Where(t => t.IsStayInClass == true).Count();
            //ToDo 老师情况
            ClassIndexViewModel model = new ClassIndexViewModel() {
                ClassName=className,
                Total=total,
                Boy=boy,
                Girl=girl,
                LiuBan=liuban,
            };
            return View(model);
        }
        [CheckPermission("Class.List")]
        public ActionResult List()
        {
            var classes = dataSvc.GetByName("ClassType");
            return View(classes);
        }
        [HttpGet]
        [CheckPermission("Class.Add")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [CheckPermission("Class.Add")]
        public ActionResult Add(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "请填写班级名称" });
            }
            bool exsits = dataSvc.Exsits("ClassType", value);
            if (exsits)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "已存在相同的班级名称" });
            }
            dataSvc.AddNew("ClassType", value);
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(adminId, "添加班级：" + value);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        //[CheckPermission("Class.Up")]
        public ActionResult Up(long id)
        {
            var classes = dataSvc.GetByName("ClassType");
            ClassUpViewModel model = new ClassUpViewModel();
            model.ClassId = id;
            model.Classes = classes;
            return View(model);
        }
        [HttpPost]
        //[CheckPermission("Class.Up")]
        public ActionResult Up(long classId, long newClassId, DateTime changeDateTime)
        {
            //判断新班级是否还有学生（未设置为留班的）
            bool exsits = stuSvc.GetAll().Any(t => t.ClassId == newClassId && t.IsStayInClass == false);
            if (exsits)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "新设置的班级仍有学生信息！" });
            }
            //批量设置（原班级已设置为留班的除外）
            var students = stuSvc.GetByClassId(classId).Where(t => t.IsStayInClass == false);
            if (students == null || students.Count() <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "原班级没有学生信息！" });
            }
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    foreach (var item in students)
                    {
                        stuSvc.UpdateClass(item.Id, newClassId);
                        stuSvc.AddGoUpRecord(item.Id, classId, newClassId, changeDateTime);//每个学生增加一条升班记录
                    }
                    //将新班级中已设置为留班的学生清除留班设置
                    students = stuSvc.GetByClassId(newClassId).Where(t => t.IsStayInClass == true);
                    if (students != null && students.Count() > 0)
                    {
                        foreach (var item in students)
                        {
                            stuSvc.StayInClass(item.Id, false);
                        }
                    }
                    //将原班级中已设置为留班的学生清除留班设置
                    students = stuSvc.GetByClassId(classId).Where(t => t.IsStayInClass == true);
                    if (students != null && students.Count() > 0)
                    {
                        foreach (var item in students)
                        {
                            stuSvc.StayInClass(item.Id, false);
                        }
                    }
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "id为" + classId + "升班到id为" + newClassId);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
        [HttpGet]
        public ActionResult BiYe(long classId)
        {
            return View(classId);
        }
        [HttpPost]
        //[CheckPermission("Class.BiYe")]
        public ActionResult BiYe(long classId,DateTime dateTime)
        {
            bool exsits = stuSvc.GetAll().Any(t => t.ClassId == classId && t.IsStayInClass == false);
            if (!exsits)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "该班级没有符合条件的学生" });
            }
            var students = stuSvc.GetByClassId(classId).Where(t => t.IsStayInClass == false).ToArray();
            foreach (var stu in students)
            {
                stuSvc.FinishSchool(stu.Id, dateTime);
            }
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(adminId, "设置班级毕业：classId=" + classId);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Class.Delete")]
        public ActionResult Delete(long id)
        {
            dataSvc.MarkDeleted(id);
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(adminId, "删除班级：classId=" + id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Class.Delete")]
        public ActionResult PatchDel(long[] selectedIds)//批量删除
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                dataSvc.MarkDeleted(id);
                long adminId = (long)AdminHelper.GetUserId(HttpContext);
                logSvc.AddNew(adminId, "删除班级：classId=" + id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        [CheckPermission("Class.Edit")]
        public ActionResult Edit(long id)
        {
            var banji = dataSvc.GetById(id);
            return View(banji);
        }
        [HttpPost]
        [CheckPermission("Class.Edit")]
        public ActionResult Edit(long id, string value)
        {
            bool exsits = dataSvc.GetByName("ClassType").Any(t => t.Id != id && t.Value == value);
            if (exsits)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "已存在相同的班级名称" });
            }
            dataSvc.UpdateValue(id, value);
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(adminId, "修改班级：classId=" + id);
            return Json(new AjaxResult { Status = "ok" });
        }

    }
}