using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class FoodAddPostModel
    {
        [Required(ErrorMessage ="名称必填")]
        public string Name { get; set; }
        [Required(ErrorMessage = "单位必填")]
        public string Unit { get; set; }
        public string Supplier { get; set; }
    }
}