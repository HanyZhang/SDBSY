using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class ParentConfig : EntityTypeConfiguration<ParentEntity>
    {
        public ParentConfig()
        {
            ToTable("T_Parents");
            Property(t => t.Name).HasMaxLength(20).IsRequired();
            Property(t => t.WorkUnit).HasMaxLength(200).IsRequired();
            Property(t => t.PhoneNum).HasMaxLength(20).IsRequired();
            HasRequired(t => t.IdCardType).WithMany().HasForeignKey(t => t.IdCardTypeId).WillCascadeOnDelete(false);
            Property(t => t.IdCardNum).HasMaxLength(30).IsOptional();
        }
    }
}
