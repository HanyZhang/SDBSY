using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.DTO;

namespace SDBSY.TeacherWeb.Models
{
    public class CertificateListViewModel
    {
        public CertificateDTO[] Certificates { get; set; }
        public CertificatePicDTO[] CertificatePics { get; set; }
    }
}