using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VueJsTest.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        // GET: Account
        public ActionResult Login(string name,string password)
        {
            if (name == "admin" && password == "123456")
                return Json("status:ok");
            else
                return Json("status:error");
        }
    }
}