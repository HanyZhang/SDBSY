using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class AdminUserDTO:BaseDTO
    {
        public string UserName { get; set; }
        public int Role { get; set; }
        public string RoleName { get; set; }
    }
}
