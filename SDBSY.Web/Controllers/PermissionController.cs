using SDBSY.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.Common;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;

namespace SDBSY.Web.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService permSvc { get; set; }
        // GET: Permission
        [HttpGet]
        [CheckPermission("Perm.Add")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [CheckPermission("Perm.Add")]
        public ActionResult Add(PermssionAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            long i = permSvc.AddPermission(model.Name, model.Description);
            if (i <= 0)
                return Json(new AjaxResult { Status = "error",ErrorMsg="已存在相同权限名称" });//权限重复
            else
                return Json(new AjaxResult { Status = "ok" });

        }
        [CheckPermission("Perm.List")]
        public ActionResult List()
        {
            var perms = permSvc.GetAll();
            return View(perms);
        }
        [HttpPost]
        [CheckPermission("Perm.Delete")]
        public ActionResult Delete(long id)
        {
            permSvc.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        [CheckPermission("Perm.Edit")]
        public ActionResult Edit(long id)
        {
            var perm = permSvc.GetById(id);
            return View(perm);
        }
        [HttpPost]
        [CheckPermission("Perm.Edit")]
        public ActionResult Edit(PermissionEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            permSvc.UpdatePermission(model.Id, model.Name, model.Description);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Perm.Delete")]
        public ActionResult DataDel(long[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                permSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}