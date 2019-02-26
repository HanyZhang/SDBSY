using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class EducateIndexViewModel
    {
        public EducateDTO Educate { get; set; }
        public CertificateDTO[] Certificates { get; set; }
    }
}