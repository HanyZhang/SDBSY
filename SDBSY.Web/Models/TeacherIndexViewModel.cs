using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.DTO;

namespace SDBSY.Web.Models
{
    public class TeacherIndexViewModel
    {
        public DataDictionaryDTO[] AdultIdCardTypes { get; set; }
        public TeacherDTO Teacher { get; set; }
    }
}