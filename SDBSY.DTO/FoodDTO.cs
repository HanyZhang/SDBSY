using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class FoodDTO:BaseDTO
    {
        public string Name { get; set; }
        /// <summary>
        /// 计量单位，如斤、袋
        /// </summary>
        public string Unit { get; set; }
        public string Supplier { get; set; }
    }
}
