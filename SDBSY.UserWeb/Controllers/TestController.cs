using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDBSY.IService;
using System.Transactions;

namespace SDBSY.UserWeb.Controllers
{
    public class TestController : Controller
    {
        public IAdminLogService logSvc { get; set; }
        public IAdminUserService adminSvc { get; set; }
        // GET: Test
        public ActionResult Index()
        {
            using (TransactionScope tran=new TransactionScope())
            {
                try
                {
                    adminSvc.AddNew("admin", "123456");
                    logSvc.AddNew(0, "测试事务");
                    tran.Complete();
                    return Content("OK");
                }
                catch(Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            
        }
    }
}