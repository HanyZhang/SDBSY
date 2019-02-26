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
    public class AdminUserController : Controller
    {
        public IAdminUserService adminSvc { get; set; }
        public IRoleService roleSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        // GET: AdminUser
        [CheckPermission("AdminUser.Index")]
        public ActionResult Index()
        {
            return View();
        }
        [CheckPermission("AdminUser.List")]
        public ActionResult List()
        {
            var admins = adminSvc.GetAll();
            return View(admins);
        }
        [HttpGet]
        [CheckPermission("AdminUser.Add")]
        public ActionResult Add()
        {
            var roles = roleSvc.GetAll();
            return View(roles);
        }
        [HttpPost]
        [CheckPermission("AdminUser.Add")]
        public ActionResult Add(AdminUserAddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            long adminUserId = adminSvc.AddNew(model.UserName, model.UserName.Substring(5), model.Role);//密码手机号后6位
            if (adminUserId <= 0)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "已存在相同账号" });//重复
            }
            else
            {
                using (var tran = new TransactionScope())//事务开启
                {
                    try
                    {
                        roleSvc.UpdateRoleIds(adminUserId, model.RoleIds);
                        //记录操作日志
                        long adminId = (long)AdminHelper.GetUserId(HttpContext);
                        logSvc.AddNew(adminId, "创建新的账号：" + adminUserId);
                        tran.Complete();
                        return Json(new AjaxResult { Status = "ok" });
                    }
                    catch (Exception ex)
                    {
                        return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                    }
                }
            }

        }
        [HttpPost]
        [CheckPermission("AdminUser.Delete")]
        public ActionResult Delete(long id)
        {
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    adminSvc.MarkDeleted(id);
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "删除账号：" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }

        [HttpPost]
        public ActionResult ResetPwd(long id)
        {
            
            using (var tran = new TransactionScope())//事务开启
            {
                try
                {
                    var admin = adminSvc.GetById(id);
                    string pwd = admin.UserName.Substring(5);
                    adminSvc.UpdatePassword(id, pwd);
                    long adminId = (long)AdminHelper.GetUserId(HttpContext);
                    logSvc.AddNew(adminId, "重置密码：id=" + id);
                    tran.Complete();
                    return Json(new AjaxResult { Status = "ok" });
                }
                catch (Exception ex)
                {
                    return Json(new AjaxResult { Status = "error", ErrorMsg = ex.Message });
                }
            }
        }
    }
}