using SDBSY.IService;
using SDBSY.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.Controllers
{
    public class AdminLogController : Controller
    {
        public IAdminLogService logSvc { get; set; }
        // GET: AdminLog
        [CheckPermission("AdminLog.List")]
        public ActionResult List()
        {
            var logs= logSvc.GetAll();
            return View(logs);
        }
    }
}