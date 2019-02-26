using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.DTO;

namespace SDBSY.TeacherWeb.Models
{
    public class InvoiceAddViewModel
    {
        public long TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Classes { get; set; }
    }
}