using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using NhapMonCNPM.Authorization.Users;
using NhapMonCNPM.MultiTenancy;
using NhapMonCNPM.EntityFrameworkCore;
using NhapMonCNPM.IoC;
using Abp.Dependency;
using Abp.ObjectMapping;

namespace NhapMonCNPM
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class NhapMonCNPMAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }
        protected IWorkScope WorkScope { get; set; }
        protected NhapMonCNPMDbContext dbContext { set; get; }
        //protected IUserService userService { set; get; }
        protected NhapMonCNPMAppServiceBase()
        {
            LocalizationSourceName = NhapMonCNPMConsts.LocalizationSourceName;
            WorkScope = IocManager.Instance.Resolve<IWorkScope>();
            ObjectMapper = IocManager.Instance.Resolve<IObjectMapper>();
            UserManager = IocManager.Instance.Resolve<UserManager>();
            TenantManager = IocManager.Instance.Resolve<TenantManager>();
            dbContext = IocManager.Instance.Resolve<NhapMonCNPMDbContext>();
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
