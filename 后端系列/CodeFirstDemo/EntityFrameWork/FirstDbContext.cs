namespace EntityFrameWork
{

    using Entity.Model;

    using System.Data.Entity;


    public class FirstDbContext : DbContext
    {

        public FirstDbContext()
            : base("name=FirstDbContext")
        {
        }

        public DbSet<User> User { get; set; }

    }


}