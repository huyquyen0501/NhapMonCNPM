using Abp.Application.Services.Dto;

namespace NhapMonCNPM.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

