using SDBSY.Common;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class UserController : Controller
    {
        public IUserService userSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        // GET: User
        [CheckPermission("User.List")]
        public ActionResult List()
        {
            var users = userSvc.GetAll();
            return View(users);
        }
        [HttpPost]
        [CheckPermission("User.List")]
        public ActionResult UpdatePassword(long id)
        {
            userSvc.UpdatePassword(id,"123456");
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            logSvc.AddNew(adminId, "重置用户密码，userId：" + id);
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}