using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web
{
    public class AdminHelper
    {
        public static long? GetUserId(HttpContextBase ctx)
        {
            return (long?)ctx.Session["LoginUserId"];
        }
        public static string GetUserRole(HttpContextBase ctx)
        {
            return (string)ctx.Session["UserRole"];
        }
    }
}