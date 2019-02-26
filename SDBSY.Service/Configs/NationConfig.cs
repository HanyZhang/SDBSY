using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class NationConfig:EntityTypeConfiguration<NationEntity>
    {
        public NationConfig()
        {
            ToTable("T_Nations");
            Property(t => t.Name).HasMaxLength(30).IsRequired();
        }
    }
}
