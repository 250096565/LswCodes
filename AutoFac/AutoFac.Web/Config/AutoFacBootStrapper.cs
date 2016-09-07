using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;

namespace AutoFac.Web.Config
{
    public class AutoFacBootStrapper
    {
        /// <summary>
        /// 配置AutoFac映射
        /// </summary>
        public static IContainer Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(
                o => o.Name.EndsWith("Services")
                ).AsImplementedInterfaces(); //查找程序集中以services结尾的类型

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())//查找程序集中以Repository结尾的类型
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces();
            //注册所有的Controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            return builder.Build();
        }
    }
}