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
            Database.SetInitializer(new DropCreateDatabaseAlways<LsDbContext>());
        }

        /// <summary>
        /// MSDN:在创建派生上下文的第一个实例时仅调用此方法一次。 
        /// 然后将缓存该上下文的模型，并且该模型适用于应用程序域
        /// 中的上下文的所有后续实例。(PS:省的手动写一大堆DbSet<Type>)
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
