using System.Web.Http;

namespace API.Controllers
{
    public class PublicController : BaseController
    {
        public PublicController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }
    }
}
