using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NhapMonCNPM.Configuration;

namespace NhapMonCNPM.Web.Host.Startup
{
    [DependsOn(
       typeof(NhapMonCNPMWebCoreModule))]
    public class NhapMonCNPMWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public NhapMonCNPMWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NhapMonCNPMWebHostModule).GetAssembly());
        }
    }
}
