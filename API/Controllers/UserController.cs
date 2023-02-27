using Application.User.CommandHandlers;
using Application.User.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : PrivateController
    {
        public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        [HttpPost]
        [Route("Get")]
        public async Task<GetUserDTO> Get(GetUserCommand request) => await CommandAsync(request);
    }
}
