using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.UserWeb
{
    public class UserHelper
    {
        public static long? GetUserId(HttpContextBase ctx)
        {
            return (long?)ctx.Session["LoginUserId"];
        }
    }
}