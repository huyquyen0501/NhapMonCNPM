using Abp.AutoMapper;
using NhapMonCNPM.Authentication.External;

namespace NhapMonCNPM.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
