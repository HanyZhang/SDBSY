using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.DTO;

namespace SDBSY.Web.Models
{
    public class InvoiceIndexViewModel
    {
        public InvoiceDTO Invoice { get; set; }
        public InvoicePicDTO[] Pics { get; set; }
    }
}