using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using NhapMonCNPM.Authorization.Roles;
using NhapMonCNPM.Authorization.Users;
using NhapMonCNPM.MultiTenancy;

namespace NhapMonCNPM.EntityFrameworkCore
{
    public class NhapMonCNPMDbContext : AbpZeroDbContext<Tenant, Role, User, NhapMonCNPMDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public NhapMonCNPMDbContext(DbContextOptions<NhapMonCNPMDbContext> options)
            : base(options)
        {
        }
    }
}
