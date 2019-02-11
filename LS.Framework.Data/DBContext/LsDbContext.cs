using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace LS.Framework.Data
{
    public class LsDbContext : DbContext
    {
        public LsDbContext()
            : base("name=LsDbContext")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// MSDN:在创建派生上下文的第一个实例时仅调用此方法一次。 
        /// 然后将缓存该上下文的模型，并且该模型适用于应用程序域
        /// 中的上下文的所有后续实例。(PS:省的手动写一大堆DbSet<Type>)
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   
            //一：数据库不存在时重新创建数据库[默认]
            //Database.SetInitializer(new CreateDatabaseIfNotExists<BaseDBContext>());
            //二：每次启动应用程序时创建数据库
            //Database.SetInitializer(new DropCreateDatabaseAlways<BaseDBContext>());
            //策略三：模型更改时重新创建数据库
#if DEBUG
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LsDbContext>());
#endif
            //策略四：从不创建数据库
            //Database.SetInitializer<BaseDBContext>(null);
            Assembly asm = Assembly.Load("LS.Framework.Models");
            var typesToRegister = asm.GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType == typeof(BaseEntity));
            foreach (var type in typesToRegister)
            {
                modelBuilder.RegisterEntityType(type);
            }

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约
            base.OnModelCreating(modelBuilder);
        }
    }
}
