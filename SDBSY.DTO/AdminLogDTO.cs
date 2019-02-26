using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class AdminLogDTO:BaseDTO
    {
        public long AdminUserId { get; set; }
        public string AdminUserName { get; set; }
        public string Message { get; set; }
    }
}
