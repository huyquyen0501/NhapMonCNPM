using System.Threading.Tasks;
using Abp.Application.Services;
using NhapMonCNPM.Sessions.Dto;

namespace NhapMonCNPM.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
