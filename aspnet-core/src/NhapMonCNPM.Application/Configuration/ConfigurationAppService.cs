using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using NhapMonCNPM.Configuration.Dto;

namespace NhapMonCNPM.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : NhapMonCNPMAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
