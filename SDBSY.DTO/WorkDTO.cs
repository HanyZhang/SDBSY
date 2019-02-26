using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class WorkDTO:BaseDTO
    {
        public long TeacherId { get; set; }
        public string TeacherName { get; set; }
        /// <summary>
        /// 工作单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
