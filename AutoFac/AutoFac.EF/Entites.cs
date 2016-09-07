using System.Data.Entity.ModelConfiguration.Conventions;
using Core.User;
using Core.UserRole;

namespace AutoFac.EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Entites : DbContext
    {
        public Entites()
            : base("name=Entites")
        {
        }


        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }


        /// <summary>
        /// Fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //全局关闭级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约

            modelBuilder.Configurations.Add(new UserMap());

            modelBuilder.Configurations.Add(new UserCardMap());

            modelBuilder.Configurations.Add(new UserRoleMap());
        }
    }

}