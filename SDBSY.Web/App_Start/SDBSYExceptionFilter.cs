using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDBSY.Web.App_Start
{
    public class SDBSYExceptionFilter : IExceptionFilter
    {
        private static ILog log = LogManager.GetLogger(nameof(SDBSYExceptionFilter));
        public void OnException(ExceptionContext filterContext)
        {
            log.Error("发生异常错误", filterContext.Exception);
        }
    }
}