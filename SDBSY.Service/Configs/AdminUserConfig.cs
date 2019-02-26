using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class AdminUserConfig:EntityTypeConfiguration<AdminUserEntity>
    {
        public AdminUserConfig()
        {
            ToTable("T_AdminUsers");
            HasMany(t => t.Roles).WithMany(r => r.AdminUsers).Map(m => m.ToTable("T_AdminUserRoles").MapLeftKey("AdminUserId").MapRightKey("RoleId"));
            Property(t => t.UserName).HasMaxLength(50).IsRequired();
            Property(t => t.PasswordHash).HasMaxLength(50).IsRequired();
            Property(t => t.PasswordSalt).HasMaxLength(50).IsRequired();
        }
    }
}
