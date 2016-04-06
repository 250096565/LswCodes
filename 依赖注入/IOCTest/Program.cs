using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCTest
{
    class Program
    {

        //容器实例
        private static IContainer _container;
        static void Main(string[] args)
        {
            InitIOC();
            var lsw = _container.Resolve<GetLSW>();
            List<Person> per = lsw.Get();
            foreach (var VARIABLE in per)
            {
                Console.WriteLine(VARIABLE.Name);
            }

        }


        private static void InitIOC()
        {
            var builder = new ContainerBuilder();
            //以接口的方式注册 
            //实例一
           // builder.RegisterType<GetLSWbyHome>().AsImplementedInterfaces();
            //实例二
            builder.RegisterType<GetLSWbyWork>().AsImplementedInterfaces();
            builder.RegisterType<GetLSW>();
            _container = builder.Build();
        }
    }
}
