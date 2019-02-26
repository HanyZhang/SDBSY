using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.DTO;

namespace SDBSY.TeacherWeb.Models
{
    public class InvoiceEditViewModel
    {
        public  InvoiceDTO Invoice { get; set; }
        public string Classes { get; set; }
        public  InvoicePicDTO[] Pics { get; set; }
    }
}