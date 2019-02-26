using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class DataDictionaryConfig:EntityTypeConfiguration<DataDictionaryEntity>
    {
        public DataDictionaryConfig()
        {
            ToTable("T_DataDictionaries");
            Property(t => t.Name).HasMaxLength(50).IsRequired();
            Property(t => t.Value).HasMaxLength(50).IsRequired();
        }
    }
}
