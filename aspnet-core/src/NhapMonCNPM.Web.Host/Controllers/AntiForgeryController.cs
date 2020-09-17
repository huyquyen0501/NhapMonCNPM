using Microsoft.AspNetCore.Antiforgery;
using NhapMonCNPM.Controllers;

namespace NhapMonCNPM.Web.Host.Controllers
{
    public class AntiForgeryController : NhapMonCNPMControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
