using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class ClassUpViewModel
    {
        public long StudentId { get; set; }
        public long[] StudentIds { get; set; }
        public long ClassId { get; set; }
        public DataDictionaryDTO[] Classes { get; set; }
    }
}