using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NhapMonCNPM.Configuration;
using NhapMonCNPM.EntityFrameworkCore;
using NhapMonCNPM.Migrator.DependencyInjection;

namespace NhapMonCNPM.Migrator
{
    [DependsOn(typeof(NhapMonCNPMEntityFrameworkModule))]
    public class NhapMonCNPMMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public NhapMonCNPMMigratorModule(NhapMonCNPMEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(NhapMonCNPMMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                NhapMonCNPMConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NhapMonCNPMMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
