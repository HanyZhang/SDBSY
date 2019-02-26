using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class GuardianConfig:EntityTypeConfiguration<GuardianEntity>
    {
        public GuardianConfig()
        {
            ToTable("T_Guardians");
            Property(t => t.Name).HasMaxLength(20).IsRequired();
            Property(t => t.PhoneNum).HasMaxLength(20).IsOptional();
            HasRequired(t => t.IdCardType).WithMany().HasForeignKey(t => t.IdCardTypeId).WillCascadeOnDelete(false);
            Property(t => t.IdCardNum).HasMaxLength(30).IsOptional();
        }
    }
}
