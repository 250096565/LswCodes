using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using StudyABP.EntityFramework;
using StudyABP.Migrations;

namespace StudyABP
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(StudyABPCoreModule))]
    public class StudyABPDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
