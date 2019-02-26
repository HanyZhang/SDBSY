using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class FoodBuyRecordDTO:BaseDTO
    {
        public long FoodId { get; set; }
        [DisplayName("进货日期")]
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime { get; set; }
        [DisplayName("食物名称")]
        public string FoodName { get; set; }
        [DisplayName("单位")]
        public string FoodUnit { get; set; }
        
        
        [DisplayName("数量")]
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }
        [DisplayName("单价（元）")]
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        [DisplayName("金额（元）")]
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        [DisplayName("备注")]
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
        public string Supplier { get; set; }
    }
}
