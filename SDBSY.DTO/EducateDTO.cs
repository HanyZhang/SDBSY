using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class EducateDTO:BaseDTO
    {
        public long TeacherId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SchoolName { get; set; }
        /// <summary>
        /// 类型(学历教育1  培训2  其他3) 
        /// </summary>
        public int Type { get; set; }
        public string TypeName { get; set; }
    }
}
