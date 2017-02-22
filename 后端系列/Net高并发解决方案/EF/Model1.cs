namespace EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model1 : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“Model1”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“EF.Model1”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“Model1”
        //连接字符串。
        public Model1()
            : base("name=default")
        {
        }

        public virtual DbSet<Person> Person { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        public string Id2 { get; set; }
        public string Name { get; set; }
    }
}