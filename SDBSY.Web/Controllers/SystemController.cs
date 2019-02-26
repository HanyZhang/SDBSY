using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.IService;
using SDBSY.Web.App_Start;
using SDBSY.Web.Models;
using SDBSY.Common;

namespace SDBSY.Web.Controllers
{
    public class SystemController : Controller
    {
        public IDataDictionaryService dataSvc { get; set; }
        public ISystemSettingService settingSvc { get; set; }
        public IAdminLogService logSvc { get; set; }
        // GET: System
        [CheckPermission("System.Index")]
        [HttpGet]
        public ActionResult Index()
        {
            //SystemSettingModel model = new SystemSettingModel();
            ////系统开关状态
            //var system = dataSvc.GetSingleByName("SystemOpen");
            //model.SystemOpen = system.Value;
            //return View(model);

            var settings = settingSvc.GetAll();
            return View(settings);


        }
        [HttpPost]
        [CheckPermission("System.Index")]
        public ActionResult Index(SystemSettingPostModel model)
        {
            long adminId = (long)AdminHelper.GetUserId(HttpContext);
            foreach (System.Reflection.PropertyInfo info in model.GetType().GetProperties())
            {
                string n = info.Name;
                string v = (string)model.GetType().GetProperty(info.Name).GetValue(model, null);
                settingSvc.Update(n,v);
                //记录操作日志

                logSvc.AddNew(adminId, "设置"+n+"为"+v);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}