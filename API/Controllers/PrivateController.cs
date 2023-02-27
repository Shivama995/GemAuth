using Application.Authentication.Attributes;
using System.Web.Http;

namespace API.Controllers
{
    [ApiAuthorization]
    public class PrivateController : BaseController
    {
        public PrivateController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }
    }
}
