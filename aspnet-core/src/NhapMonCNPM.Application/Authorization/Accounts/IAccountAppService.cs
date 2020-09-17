using System.Threading.Tasks;
using Abp.Application.Services;
using NhapMonCNPM.Authorization.Accounts.Dto;

namespace NhapMonCNPM.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
