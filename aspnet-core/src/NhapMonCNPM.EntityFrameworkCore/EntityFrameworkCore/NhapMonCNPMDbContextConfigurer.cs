using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace NhapMonCNPM.EntityFrameworkCore
{
    public static class NhapMonCNPMDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<NhapMonCNPMDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<NhapMonCNPMDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
