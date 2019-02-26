using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.Common;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;

namespace SDBSY.Web.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService roleSvc { get; set; }
        public IPermissionService permSvc { get; set; }
        // GET: Role
        [HttpGet]
        [CheckPermission("Role.Add")]
        public ActionResult Add()
        {
            var perms = permSvc.GetAll();
            return View(perms);
        }
        [HttpPost]
        [CheckPermission("Role.Add")]
        public ActionResult Add(RoleAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            long roleId = roleSvc.AddNew(model.Name);
            if(roleId<=0)
            {
                return Json(new AjaxResult { Status = "error",ErrorMsg="角色名称已存在" });
            }
            permSvc.AddPermIds(roleId, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        [CheckPermission("Role.Edit")]
        public ActionResult Edit(long id)
        {
            var role = roleSvc.GetById(id);
            var rolePerms = permSvc.GetByRoleId(id);
            var allPerms = permSvc.GetAll();
            RoleEditGetModel model = new RoleEditGetModel();
            model.Role = role;
            model.RolePerms = rolePerms;
            model.AllPerms = allPerms;
            return View(model);
        }
        [HttpPost]
        [CheckPermission("Role.Edit")]
        public ActionResult Edit(RoleEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            roleSvc.Update(model.Id, model.Name);
            permSvc.UpdatePermIds(model.Id, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Role.List")]
        public ActionResult List()
        {
            var roles = roleSvc.GetAll();
            return View(roles);
        }
        [HttpPost]
        [CheckPermission("Role.Delete")]
        public ActionResult Delete(long id)
        {
            roleSvc.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        [CheckPermission("Role.Delete")]
        public ActionResult DataDel(long[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "未选中任何信息" });
            }
            foreach (long id in selectedIds)
            {
                roleSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}