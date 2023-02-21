using Application.Authentication.Attributes;

namespace API.Controllers
{
    [ApiAuthorization]
    public class PrivateController : BaseController
    {
        public PrivateController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }
    }
}
