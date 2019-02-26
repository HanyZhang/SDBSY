using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
    public class WorkEntity:BaseEntity
    {
        public long TeacherId { get; set; }
        public virtual TeacherEntity Teacher { get; set; }
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
