using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
    public class FoodEntity:BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 计量单位，如斤、袋
        /// </summary>
        public string Unit { get; set; }
        public string Supplier { get; set; }
        public virtual ICollection<FoodBuyRecordEntity> FoodBuyRecords { get; set; } = new List<FoodBuyRecordEntity>(); 
    }
}
