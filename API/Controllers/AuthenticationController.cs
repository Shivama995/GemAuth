using Application.Authentication.CommandHandlers;
using Application.Authentication.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        [HttpPost]
        [Route("Credentials/Verify")]
        public async Task<VerifyCredentialsDTO> VerifyCredentials(VerifyCredentialsCommand request) =>
            await CommandAsync(request);
            
    }
}
