using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class ClassIndexViewModel
    {
        public string ClassName { get; set; } 
        public int Total { get; set; }
        public int Boy { get; set; }
        public int Girl { get; set; }
        public int LiuBan { get; set; }
    }
}