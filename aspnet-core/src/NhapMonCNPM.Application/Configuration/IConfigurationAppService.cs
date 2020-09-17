using System.Threading.Tasks;
using NhapMonCNPM.Configuration.Dto;

namespace NhapMonCNPM.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
