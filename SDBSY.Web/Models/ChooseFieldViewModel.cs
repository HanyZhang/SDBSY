using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class ChooseFieldViewModel
    {
        public long[] SelectIds { get; set; }
        public long ClassId { get; set; }
        public int Status { get; set; }
        public Dictionary<string,string> Dictionary { get; set; }
    }
}