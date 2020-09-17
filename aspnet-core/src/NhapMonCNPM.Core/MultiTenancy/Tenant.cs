using Abp.MultiTenancy;
using NhapMonCNPM.Authorization.Users;

namespace NhapMonCNPM.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
