namespace EntityFrameWork
{
    using Dm.AiMa.Domain;
    using Dm.AiMa.Domain.Activities;
    using Dm.AiMa.Domain.Banks;
    using Dm.AiMa.Domain.Singles;
    using Dm.AiMa.Domain.Tags;
    using Entity.Model;

    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class FirstDbContext : DbContext
    {

        public FirstDbContext()
            : base("name=FirstDbContext")
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();



            modelBuilder.Entity<User>().HasMany(a => a.UserRoles).WithMany(o => o.Users).Map(o =>
                {
                    o.ToTable("UserAndUserRoleMap");
                    o.MapLeftKey("UserId");
                    o.MapRightKey("UserRoleId");
                });



            modelBuilder.Entity<Tearcher>().HasMany(o => o.Students).WithOptional(o => o.Tearcher);


            modelBuilder.ComplexType<Activity_System.ActionData>();

            //密码库和系统活动记录，删除密码库时删除所有活动记录
            //modelBuilder.Entity<Activity_System>()
            //    .HasRequired(x => x.Bank)
            //    .WithMany(x => x.SystemActivities)
            //    .HasForeignKey(x => x.BankId)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Single_Data>()
            //    .HasRequired(x => x.Bank)
            //    .WithMany(x => x.Datas)
            //    .HasForeignKey(x => x.BankId)
            //    .WillCascadeOnDelete(true);

            modelBuilder.Entity<Single_Authorization>()
            .HasRequired(x => x.SingleData)
            .WithMany(x => x.Single_Authorizations)
            .HasForeignKey(x => x.SingleDataId)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<Bank_Authorization>()
                .HasRequired(x => x.Bank)
                .WithMany(x => x.BankAuthorizations)
                .HasForeignKey(x => x.BankId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Tag_Own>()
                .HasRequired(x => x.Bank)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.BankId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Tag_Authorization>()
                .HasRequired(x => x.Tag)
                .WithMany(x => x.TagAuthorizations)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Tag_Own>()
                    .HasMany(x => x.SingleDatas)
                    .WithMany(x => x.Tags)
                    .Map(x =>
                    {
                        x.ToTable("TagAndBankData", SchemaConsts.Mapping);
                        x.MapLeftKey("TagId");
                        x.MapRightKey("DataId");
                    });

        }

    }


}