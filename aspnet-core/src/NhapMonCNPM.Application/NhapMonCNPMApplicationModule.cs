using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NhapMonCNPM.Authorization;

namespace NhapMonCNPM
{
    [DependsOn(
        typeof(NhapMonCNPMCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class NhapMonCNPMApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<NhapMonCNPMAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(NhapMonCNPMApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
