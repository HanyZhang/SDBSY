using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class StudentListViewModel
    {
        public StudentDTO[] Students { get; set; }
        public DataDictionaryDTO[] Classes { get; set; }
        public long ClassId { get; set; }
    }
}