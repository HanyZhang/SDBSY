using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class SystemSettingDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Discription { get; set; }
    }
}
