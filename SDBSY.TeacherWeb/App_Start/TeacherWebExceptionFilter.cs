using System.Web.Mvc;
using log4net;

namespace SDBSY.TeacherWeb.App_Start
{
    public class TeacherWebExceptionFilter : IExceptionFilter
    {
        private static ILog log = LogManager.GetLogger(nameof(TeacherWebExceptionFilter));
        public void OnException(ExceptionContext filterContext)
        {
            log.Error("发生异常错误", filterContext.Exception);
        }
    }
}