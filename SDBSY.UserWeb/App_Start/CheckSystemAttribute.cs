using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.UserWeb.App_Start
{
    //此Attribute可应用到方法上，不能使用多个
    /// <summary>
    /// 检查信息采集系统是否开放
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckSystemAttribute : Attribute
    {

    }
}