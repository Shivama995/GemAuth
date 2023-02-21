namespace API.Controllers
{
    public class UserContoller : PrivateController
    {
        public UserContoller(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }
    }
}
