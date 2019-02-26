using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class AdminLogConfig:EntityTypeConfiguration<AdminLogEntity>
    {
        public AdminLogConfig()
        {
            ToTable("T_AdminLogs");
            HasRequired(t => t.AdminUser).WithMany().HasForeignKey(t => t.AdminUserId).WillCascadeOnDelete(false);
            Property(t => t.Message).IsRequired();
        }
    }
}
