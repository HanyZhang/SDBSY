using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
    public class DataDictionaryEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
