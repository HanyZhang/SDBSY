using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.DTO;

namespace SDBSY.TeacherWeb.Models
{
    public class InvoiceListViewModel
    {
        public InvoiceDTO[] Invoices { get; set; }
        public InvoicePicDTO[] InvoicePics { get; set; }
    }
}