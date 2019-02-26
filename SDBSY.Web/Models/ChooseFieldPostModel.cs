using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class ChooseFieldPostModel
    {
        public long[] SelectedIds { get; set; }
        
        public string[] DicFields { get; set; }
    }
}