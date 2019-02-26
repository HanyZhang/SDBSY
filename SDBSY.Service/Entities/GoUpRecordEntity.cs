using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
    /// <summary>
    /// 幼儿升班记录
    /// </summary>
    public class GoUpRecordEntity : BaseEntity
    {
        public long StudentId { get; set; }
        public virtual StudentEntity Student { get; set; }
        public string OldClassName { get; set; }
        public string NewClassName { get; set; }
        public DateTime Time { get; set; }
    }
}
