using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class WorkListViewModel
    {
        public long TeacherId { get; set; }
        public WorkDTO[] Works { get; set; }
    }
}