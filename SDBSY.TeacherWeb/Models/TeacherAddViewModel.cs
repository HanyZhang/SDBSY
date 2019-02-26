using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.Common;
using SDBSY.DTO;

namespace SDBSY.TeacherWeb.Models
{
    public class TeacherAddViewModel
    {
        public long AdminUserId { get; set; }
        public string IdCardTypes { get; set; }
        public string HighestEducation { get; set; }
        public string TeacherType { get; set; }
        public string ZhiCheng { get; set; }
        public string EmployType { get; set; }
        public string TeacherCardType { get; set; }
        public string PostType { get; set; }
    }
}