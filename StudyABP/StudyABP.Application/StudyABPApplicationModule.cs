using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace StudyABP
{
    [DependsOn(typeof(StudyABPCoreModule), typeof(AbpAutoMapperModule))]
    public class StudyABPApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
