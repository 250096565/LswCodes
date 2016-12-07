using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;

[assembly: OwinStartup(typeof(OwinHost.Startup))]

namespace OwinHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage("/");
            app.UseErrorPage();
            app.Run(context =>
            {
                //将请求记录在控制台
                Trace.WriteLine(context.Request.Uri);
                //显示错误页
                if (context.Request.Path.ToString().Equals("/error"))
                {
                    throw new Exception("抛出异常");
                }
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello World");
            });
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
