using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.IService;
using SDBSY.TeacherWeb.App_Start;

namespace SDBSY.TeacherWeb.Controllers
{
    [CheckLogin]
    public class HomeController : Controller
    {
        public IAdminUserService adminSvc { get; set; }
        // GET: Home
        public ActionResult Index()
        {
            var id = (long) AdminHelper.GetUserId(HttpContext);
            var admin = adminSvc.GetById(id);
            string substring = admin.UserName.Substring(3, 4);
            admin.UserName = admin.UserName.Replace(substring, "****");
            return View(admin);
        }
    }
}