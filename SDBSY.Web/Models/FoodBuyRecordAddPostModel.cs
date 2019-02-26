using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class FoodBuyRecordAddPostModel
    {
        public long FoodId { get; set; }
        [Required(ErrorMessage = "入库时间必填")]
        public DateTime BuyTime { get; set; }
        [Required(ErrorMessage = "单价必填")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "数量必填")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage ="总价必填")]
        public decimal TotalPrice { get; set; }
        public string Remark { get; set; }
    }
}