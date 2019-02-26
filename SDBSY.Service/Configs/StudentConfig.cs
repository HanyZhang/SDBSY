using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class StudentConfig:EntityTypeConfiguration<StudentEntity>
    {
        public StudentConfig()
        {
            ToTable("T_Students");
            Property(t => t.Name).HasMaxLength(50).IsRequired();
            //可为空外键用HasOptional，不能为空的用HasRequired
            HasOptional(u => u.Class).WithMany().HasForeignKey(u => u.ClassId).WillCascadeOnDelete(false);
            HasRequired(t => t.IdCardType).WithMany().HasForeignKey(t => t.IdCardTypeId).WillCascadeOnDelete(false);
            Property(t => t.IdCardNum).HasMaxLength(20).IsOptional();
            HasRequired(t => t.BloodType).WithMany().HasForeignKey(t => t.BloodTypeId).WillCascadeOnDelete(false);
            HasRequired(t => t.Country).WithMany().HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);
            HasOptional(t => t.Nation).WithMany().HasForeignKey(t => t.NationId).WillCascadeOnDelete(false);
            HasRequired(t => t.Identity).WithMany().HasForeignKey(t => t.IdentityId).WillCascadeOnDelete(false);
            HasOptional(t => t.BirthPlace).WithMany().HasForeignKey(t => t.BirthPlaceId).WillCascadeOnDelete(false);
            Property(t => t.PriginPlace).HasMaxLength(200).IsRequired();
            HasOptional(t => t.HuKouPlace).WithMany().HasForeignKey(t => t.HuKouPlaceId).WillCascadeOnDelete(false);
            HasOptional(t => t.FeiNongHuKouType).WithMany().HasForeignKey(t => t.FeiNongHuKouTypeId).WillCascadeOnDelete(false);
            HasOptional(t => t.HuKouXingZhi).WithMany().HasForeignKey(t => t.HuKouXingZhiId).WillCascadeOnDelete(false);
            Property(t => t.HomePlace).HasMaxLength(250).IsRequired();
            HasRequired(t => t.StudyType).WithMany().HasForeignKey(t => t.StudyTypeId).WillCascadeOnDelete(false);
            HasRequired(t => t.IsStayAtHome).WithMany().HasForeignKey(t => t.IsStayAtHomeId).WillCascadeOnDelete(false);
            HasRequired(t => t.HealthyType).WithMany().HasForeignKey(t => t.HealthyTypeId).WillCascadeOnDelete(false);
            HasOptional(t => t.DisabilityType).WithMany().HasForeignKey(t => t.DisabilityTypeId).WillCascadeOnDelete(false);
            HasRequired(t => t.Guardian).WithMany().HasForeignKey(t => t.GuardianId).WillCascadeOnDelete(false);
            HasOptional(t => t.Father).WithMany().HasForeignKey(t => t.FatherId).WillCascadeOnDelete(false);
            HasOptional(t => t.Mother).WithMany().HasForeignKey(t => t.MotherId).WillCascadeOnDelete(false);
            Property(t => t.OtherTel).HasMaxLength(20).IsRequired();
            Property(t => t.BankCardNum).HasMaxLength(30).IsRequired();
            HasOptional(t => t.User).WithMany().HasForeignKey(t => t.UserId).WillCascadeOnDelete(false);
        }
    }
}
