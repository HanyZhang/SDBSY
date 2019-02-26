using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class GoUpRecordDTO:BaseDTO
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public string OldClassName { get; set; }
        public string NewClassName { get; set; }
        public DateTime Time { get; set; }
    }
}
