using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class EducateListViewModel
    {
        public long TeacherId { get; set; }
        public EducateDTO[] Educates { get; set; }
    }
}