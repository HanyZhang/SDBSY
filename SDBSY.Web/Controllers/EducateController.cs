using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCarvings.Piczard;
using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;

namespace SDBSY.Web.Controllers
{
    public class EducateController : Controller
    {
        public ITeacherService teacherSvc { get; set; }
        // GET: Educate
        [CheckPermission("Educate.Index")]
        public ActionResult Index(long id)
        {
            var educate = teacherSvc.GetByEducateId(id);
            return View(educate);
        }
        [CheckPermission("Educate.List")]
        public ActionResult List(long teacherId)
        {
            var educates = teacherSvc.GetEducates(teacherId);
            EducateListViewModel model = new EducateListViewModel();
            model.Educates = educates;
            model.TeacherId = teacherId;
            return View(model);
        }
        [HttpGet]
        [CheckPermission("Educate.Add")]
        public ActionResult Add(long teacherId)
        {
            return View(teacherId);
        }
        [HttpPost]
        [CheckPermission("Educate.Add")]
        public ActionResult Add(EducateDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            teacherSvc.AddNewEducate(dto);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        [CheckPermission("Educate.Edit")]
        public ActionResult Edit(long educateId)
        {
            var educate = teacherSvc.GetByEducateId(educateId);
            return View(educate);
        }
        [HttpPost]
        [CheckPermission("Educate.Edit")]
        public ActionResult Edit(EducateEditPostModel model)
        {
            if(!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            teacherSvc.UpdateEducate(model.Id, model.SchoolName,model.Type, model.StartTime, model.EndTime);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Educate.Delete")]
        public ActionResult Delete(long id)
        {
            teacherSvc.DelEducate(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Educate.Delete")]
        public ActionResult PatchDel(long[] selectedIds)//批量删除
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                teacherSvc.DelEducate(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}