using Application.Authentication.Attributes;
using Application.Org.CommandHandlers;
using Application.Org.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrgController : PublicController
    {
        public OrgController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        [HttpPost]
        [Route("Register")]
        public async Task<RegisterAppDTO> RegisterApp(RegisterAppCommand request) =>
            await CommandAsync(request);
    }
}
