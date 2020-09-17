using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NhapMonCNPM.MultiTenancy.Dto;

namespace NhapMonCNPM.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

