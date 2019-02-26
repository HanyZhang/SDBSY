using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    public class SystemSettingConfig: EntityTypeConfiguration<SystemSettingEntity>
    {
        public SystemSettingConfig()
        {
            ToTable("T_SystemSettings");
        }
    }
}
