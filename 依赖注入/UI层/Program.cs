using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 实体层;
using 服务层;
using 逻辑层;

namespace UI层
{
    class Program
    {
        static IContainer container;
        static void Main(string[] args)
        {
            InitIOC();
            UserManager user = container.Resolve<UserManager>();
            List<Person> listPer = user.Query();


        }


        public static void InitIOC()
        {
            var builder = new ContainerBuilder();
            //以接口的方式注册 

            builder.RegisterType<UserManager>().AsImplementedInterfaces();
            container = builder.Build();
        }
    }
}

