using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.TeacherWeb.App_Start
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CheckLoginAttribute:Attribute
    {
    }
}