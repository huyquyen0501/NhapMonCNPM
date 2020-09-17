using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NhapMonCNPM.Configuration;
using NhapMonCNPM.Web;

namespace NhapMonCNPM.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class NhapMonCNPMDbContextFactory : IDesignTimeDbContextFactory<NhapMonCNPMDbContext>
    {
        public NhapMonCNPMDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NhapMonCNPMDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            NhapMonCNPMDbContextConfigurer.Configure(builder, configuration.GetConnectionString(NhapMonCNPMConsts.ConnectionStringName));

            return new NhapMonCNPMDbContext(builder.Options);
        }
    }
}
