using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class PalceConfig : EntityTypeConfiguration<PlaceEntity>
    {
        public PalceConfig()
        {
            ToTable("T_Places");
            Property(t => t.Name).HasMaxLength(50).IsRequired();
            Property(t => t.Code).HasMaxLength(50).IsRequired();
        }
    }
}
