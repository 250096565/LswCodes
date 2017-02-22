using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Owin;
using Net高并发解决方案.RabbitMq;
using Owin;

[assembly: OwinStartup(typeof(Net高并发解决方案.Startup))]

namespace Net高并发解决方案
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire");


            //添加三个后台任务也就是三个consumer
            //BackgroundJob.Enqueue(() => MqConsumber.ConsumeQueue());
            //BackgroundJob.Enqueue(() => MqConsumber.ConsumeQueue());
            //BackgroundJob.Enqueue(() => MqConsumber.ConsumeQueue());
        }
    }
}
