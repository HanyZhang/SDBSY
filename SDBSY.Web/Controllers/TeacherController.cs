using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class TeacherController : Controller
    {
        public ITeacherService teacherSvc { get; set; }
        public IAdminUserService adminSvc { get; set; }
        public IDataDictionaryService dataSvc { get; set; }
        // GET: Teacher
        [CheckPermission("Teacher.Index")]
        public ActionResult Index(long id)
        {
            //long onLineAdminId = (long)AdminHelper.GetUserId(HttpContext);
            //var admin = adminSvc.GetById(onLineAdminId);
            var teac = teacherSvc.GetById(id);
           
            var adultIdCardTypes = dataSvc.GetByName("AdultIdCardType");//成人证件类型
            TeacherIndexViewModel model = new TeacherIndexViewModel() {
                AdultIdCardTypes=adultIdCardTypes,
                Teacher=teac,
            };
            return View(model);
        }
        //[HttpGet]
        //[CheckPermission("Teacher.Add")]
        //public ActionResult Add()
        //{
        //    var adultIdCardTypes = dataSvc.GetByName("AdultIdCardType");//成人证件类型
        //    return View(adultIdCardTypes);
        //}
        //[HttpPost]
        //[CheckPermission("Teacher.Add")]
        //public ActionResult Add(TeacherDTO dto)
        //{
        //    teacherSvc.AddNew(dto);
        //    return Json(new AjaxResult {Status="ok" });
        //}
        [CheckPermission("Teacher.List")]
        public ActionResult List()
        {
            var teachers = teacherSvc.GetAll();
            return View(teachers);
        }
        //[HttpGet]
        //[CheckPermission("Teacher.Edit")]
        //public ActionResult Edit(long id)
        //{
        //    var teac = teacherSvc.GetById(id);
        //    if (teac == null)
        //    {
        //        return RedirectToAction("Add", "Teacher");
        //    }
        //    return View(teac);
        //}
        //[HttpPost]
        //[CheckPermission("Teacher.Edit")]
        //public ActionResult Edit(TeacherDTO dto)
        //{
        //    teacherSvc.Update(dto);
        //    return Json(new AjaxResult { Status = "ok" });
        //}
        [HttpPost]
        [CheckPermission("Teacher.Delete")]
        public ActionResult Delete(long id)
        {
            teacherSvc.DelTeacher(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Teacher.Delete")]
        public ActionResult PatchDel(long[] selectedIds)//批量删除
        {
            if(selectedIds==null||selectedIds.Length<=0)
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg="未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                teacherSvc.DelTeacher(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpPost]
        public ActionResult ExportExcleWithModel(long[] selectedIds)
        {
            var teachers = GetTeachers(selectedIds);
            if (teachers == null || teachers.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "没有符合条件的教师信息" });
            }

            var cers = teacherSvc.GetCertificates(selectedIds);
            var tras = teacherSvc.GetTrainings(selectedIds);
            var path = string.Empty;
            WriteToExcel(teachers,cers,tras,out path);
            path = path.Remove(0, 1);
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            //logSvc.AddNew(adminId, "用全省模板导出幼儿信息");
            return Json(new AjaxResult { Status = "ok", Data = path });
        }

        private void WriteToExcel(TeacherDTO[] teachers,CertificateDTO[] cers,TrainingDTO[] tras,out string path)
        {
            var guid = Guid.NewGuid();

            var filePath = string.Empty;
            var filename = guid.ToString() + ".xls";
            path = "~/Download/File/" + DateTime.Now.ToString("yyyyMMdd") + "/" + filename;
            //临时存放路径
            filePath = Server.MapPath(path);
            new FileInfo(filePath).Directory.Create(); //尝试创建路径，即使存在也不会报错
            //Excel模版
            string masterPath = Server.MapPath("~/Files/teacherModel.xls");
            //复制Excel模版
            System.IO.File.Copy(masterPath, filePath);

            // 先把文件的属性读取出来 
            FileAttributes attrs = System.IO.File.GetAttributes(filePath);

            // 下面表达式中的 1 是 FileAttributes.ReadOnly 的值 
            // 此表达式是把 ReadOnly 所在的位改成 0, 
            attrs = (FileAttributes)((int)attrs & ~(1));

            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = new HSSFWorkbook(file);

            //the first sheet 基本信息
            ISheet sheet1 = hssfworkbook.GetSheet("基本信息");
            for (int i = 0; i < teachers.Count(); i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(teachers[i].Name); //姓名
                row.CreateCell(1).SetCellValue(teachers[i].GenderName); //性别
                row.CreateCell(2).SetCellValue(teachers[i].BirthDay.ToString("yyyyMMdd")); //出生日期
                row.CreateCell(3).SetCellValue(teachers[i].TelPhone); //电话
                row.CreateCell(4).SetCellValue(teachers[i].IdCardTypeName); //证件类型
                row.CreateCell(5).SetCellValue(teachers[i].IdCardNum); //证件号码
                row.CreateCell(6).SetCellValue(teachers[i].SchoolName); //毕业学校
                row.CreateCell(7).SetCellValue(teachers[i].HighestEducationName); //最高学历
                row.CreateCell(8).SetCellValue(teachers[i].HomePlace); //联系地址
                row.CreateCell(9).SetCellValue(teachers[i].TeacherTypeName); //叫职工类别
                row.CreateCell(10).SetCellValue(teachers[i].PostTypeName); //任职岗位
                row.CreateCell(11).SetCellValue(teachers[i].IsLeaderStr); //园长资格
                row.CreateCell(12).SetCellValue(teachers[i].LeaderAuthUnit); //园长资格认证单位
                row.CreateCell(13).SetCellValue(teachers[i].IsPreschoolEducationStr); //幼教专业
                row.CreateCell(14).SetCellValue(teachers[i].ZhiChengName); //职称
                row.CreateCell(15).SetCellValue(teachers[i].EmployTypeName); //教师类型
                row.CreateCell(16).SetCellValue(teachers[i].BasePay.ToString()); //基本工资
                row.CreateCell(17).SetCellValue(teachers[i].IsYangLaoStr); //养老
                row.CreateCell(18).SetCellValue(teachers[i].IsYiLiaoStr); //医疗
                row.CreateCell(19).SetCellValue(teachers[i].IsShiYeStr); //失业
                row.CreateCell(20).SetCellValue(teachers[i].IsGongShangStr); //工伤
                row.CreateCell(21).SetCellValue(teachers[i].IsShengYuStr); //生育
                row.CreateCell(22).SetCellValue(teachers[i].IsGongJiJinStr); //公积金
                row.CreateCell(23).SetCellValue(teachers[i].WeiGouMai); //未购买
                row.CreateCell(24).SetCellValue(teachers[i].ComeDateStr); //来源日期
                row.CreateCell(25).SetCellValue(teachers[i].WorkStartTimeStr); //任职开始
                row.CreateCell(26).SetCellValue(teachers[i].WorkEndTimeStr); //任职结束
                row.CreateCell(27).SetCellValue(teachers[i].HasTeacherCardStr); //是否有教师资格证
                row.CreateCell(28).SetCellValue(teachers[i].TeacherCardTypeName); //资格证种类
                row.CreateCell(29).SetCellValue(teachers[i].TeacherCardNum); //资格证编号
                row.CreateCell(30).SetCellValue(teachers[i].TeacherCardAwardUnit); //颁发机构
                row.CreateCell(31).SetCellValue(teachers[i].TeacherCardAwardTimeStr); //颁发日期
            }

            sheet1.ForceFormulaRecalculation = true;

            //the second sheet 证书

            ISheet sheet2 = hssfworkbook.GetSheet("证书情况");
            for (int i = 0; i < cers.Count(); i++)
            {
                IRow row = sheet2.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(cers[i].TeacherName); //姓名
                row.CreateCell(1).SetCellValue(cers[i].Name); //名称
                row.CreateCell(2).SetCellValue(cers[i].AwardUnit); //发证单位
                row.CreateCell(3).SetCellValue(cers[i].AwardDateTimeStr); //时间
                row.CreateCell(4).SetCellValue(cers[i].Number); //编号
            }

            sheet2.ForceFormulaRecalculation = true;

            //培训情况
            ISheet sheet3 = hssfworkbook.GetSheet("培训情况");
            for (int i = 0; i < tras.Count(); i++)
            {
                IRow row = sheet3.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(tras[i].TeacherName); //姓名
                row.CreateCell(1).SetCellValue(tras[i].Year); //年度
                row.CreateCell(2).SetCellValue(tras[i].UnitName); //机构
                row.CreateCell(3).SetCellValue(tras[i].TrainingContent); //项目
                row.CreateCell(4).SetCellValue(tras[i].TrainingLevelName); //级别
                row.CreateCell(5).SetCellValue(tras[i].TrainingTypeName); //形式
                row.CreateCell(6).SetCellValue(tras[i].TrainingTime); //学时
            }

            sheet3.ForceFormulaRecalculation = true;

            using (FileStream filess = System.IO.File.OpenWrite(filePath))
            {
                hssfworkbook.Write(filess);
            }
        }
        private TeacherDTO[] GetTeachers(long[] ids)
        {
            var teachers = teacherSvc.GetById(ids);
            return teachers;
        }
    }
}