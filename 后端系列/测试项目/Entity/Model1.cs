namespace Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .Property(e => e.cloudid)
                .IsUnicode(false);

            modelBuilder.Entity<Photo>()
                .Property(e => e.roleid)
                .IsUnicode(false);

            modelBuilder.Entity<Photo>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Photo>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Photo>()
                .Property(e => e.subtype)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ClassID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Spec)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UNIT)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Supplier)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.PMID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.levels)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.describe)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.brief)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.memo)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductAttributes)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ShortDescribes)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.SendDate)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.AppDescribe)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.VideoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ZMDescribes)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ZMShortDescribes)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.CloudID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.shopname)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.mobile)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.addr)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.postcode)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.promoter)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.bindingId)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.bindingId2)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.bank)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.branch)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.cardnumber)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.cardname)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.idcard)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.regionManagerID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.ZJID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.enterpriseInfo)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.memo)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.authChar)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.productID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.orderid)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.vipcard)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.qq)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.weixin)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.weibo)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.nickname)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.transferStat)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.keystring)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.keystring1)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.keystring1Info)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.tracenumber)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.FixedTelephone)
                .IsUnicode(false);
        }
    }
}
