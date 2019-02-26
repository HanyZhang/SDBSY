using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
    public class AdminUserEntity:BaseEntity
    {
        public string UserName { get; set; }
        public int Role { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
