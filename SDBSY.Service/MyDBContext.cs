using log4net;
using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service
{
    public class MyDBContext:DbContext
    {
        //ILog ILogger,
        private static ILog log = LogManager.GetLogger(typeof(MyDBContext));

        public MyDBContext():base("name=connstr")
            //name=conn1表示使用连接字符串中名字为conn1的去连接数据库
        {
            Database.SetInitializer<MyDBContext>(null);
            this.Database.Log = (sql) => {
                log.DebugFormat("EF执行SQL：{0}", sql);
            };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<AdminLogEntity> AdminLogs { get; set; }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<CertificateEntity> Certificates { get; set; }
        public DbSet<CertificatePicEntity> CertificatePics { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<DataDictionaryEntity> DataDictionaries { get; set; }
        public DbSet<GuardianEntity> Guardians { get; set; }
        public DbSet<NationEntity> Nations { get; set; }
        public DbSet<ParentEntity> Parents { get; set; }
        public DbSet<PlaceEntity> Places { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        
        public DbSet<SystemSettingEntity> SystemSettings { get; set; }
        public DbSet<EducateEntity> Educates { get; set; }
        public DbSet<WorkEntity> Works { get; set; }
        public DbSet<GoUpRecordEntity> GoUpRecords { get; set; }
        public DbSet<FoodEntity> Foods { get; set; }
        public DbSet<FoodBuyRecordEntity> FoodBuyRecords { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<NewPlaceEntity> NewPlaces { get; set; }
        public DbSet<TrainingEntity> Trainings { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<InvoicePicEntity> InvoicePics { get; set; }
        public DbSet<GoodsEntity> Goods { get; set; }
        public DbSet<GoodsBuyRecordEntity> GoodsBuyRecords { get; set; }
        public DbSet<GoodsApplyRecordEntity> GoodsApplyRecords { get; set; }
    }
}
