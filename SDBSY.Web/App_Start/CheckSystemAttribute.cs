using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.App_Start
{
    //此Attribute可应用到方法上，不能使用多个
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class CheckSystemAttribute:Attribute
    {

    }
}