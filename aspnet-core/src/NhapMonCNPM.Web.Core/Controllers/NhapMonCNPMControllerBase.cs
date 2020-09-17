using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace NhapMonCNPM.Controllers
{
    public abstract class NhapMonCNPMControllerBase: AbpController
    {
        protected NhapMonCNPMControllerBase()
        {
            LocalizationSourceName = NhapMonCNPMConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
