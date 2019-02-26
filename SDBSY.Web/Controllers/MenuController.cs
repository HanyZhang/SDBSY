using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.Common;
using SDBSY.IService;
using SDBSY.Web.App_Start;

namespace SDBSY.Web.Controllers
{
    public class MenuController : Controller
    {
        public IMenuService menuSvc { get; set; }
        // GET: Menu
        //[CheckPermission("Menu.List")]
        public ActionResult List()
        {
            var menus = menuSvc.GetAll();
            return View(menus);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Add(string name)
        {
            return Json(new AjaxResult {Status = "ok"});
        }
    }
}