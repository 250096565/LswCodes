using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using AutoFac.EF;
using AutoFac.Web.Config;

namespace AutoFac.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Entites>());

            var container = AutoFacBootStrapper.Init();
           
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));//更改了MVC中的注入方式

        }
    }
}
