using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
    public class ParentEntity:BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnit { get; set; }
        public string PhoneNum { get; set; }
        /// <summary>
        /// 父母证件类型
        /// </summary>
        public long IdCardTypeId { get; set; }
        public virtual DataDictionaryEntity IdCardType { get; set; }
        public string IdCardNum { get; set; }

    }
}
