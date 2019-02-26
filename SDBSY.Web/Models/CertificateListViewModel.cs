using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class CertificateListViewModel
    {
        public long EducateId { get; set; }
        public CertificateDTO[] Certificates { get; set; }
    }
}