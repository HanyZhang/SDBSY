using SDBSY.Common;
using SDBSY.DTO;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class WorkController : Controller
    {
        public ITeacherService teacherSvc { get; set; }
        // GET: Work
        [CheckPermission("Work.Index")]
        public ActionResult Index(long id)
        {
            var model = teacherSvc.GetByWorkId(id);
            return View(model);
        }
        [CheckPermission("Work.List")]
        public ActionResult List(long teacherId)
        {
            var works = teacherSvc.GetWorks(teacherId);
            var model = new WorkListViewModel() {
                TeacherId=teacherId,
                Works=works,
            };
            return View(model);
        }
        [HttpGet]
        [CheckPermission("Work.Add")]
        public ActionResult Add(long teacherId)
        {
            return View(teacherId);
        }
        [HttpPost]
        [CheckPermission("Work.Add")]
        public ActionResult Add(WorkDTO dto)
        {
            teacherSvc.AddNewWork(dto);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        [CheckPermission("Work.Edit")]
        public ActionResult Edit(long workId)
        {
            var model = teacherSvc.GetByWorkId(workId);
            return View(model);
        }
        [HttpPost]
        [CheckPermission("Work.Edit")]
        public ActionResult Edit(WorkEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            teacherSvc.UpdateWork(model.Id, model.UnitName, model.JobName, model.StartTime, model.EndTime);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Work.Delete")]
        public ActionResult Delete(long id)
        {
            teacherSvc.DelWork(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Work.Delete")]
        public ActionResult PatchDel(long[] selectedIds)//批量删除
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                teacherSvc.DelWork(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}