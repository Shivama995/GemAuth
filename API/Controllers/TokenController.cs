using Application.Token.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TokenController : BaseController
    {
        ILoginTokenService _LoginTokenService;
        public TokenController(IHttpContextAccessor httpContextAccessor,
            ILoginTokenService loginTokenService) : base(httpContextAccessor) 
        {
            _LoginTokenService = loginTokenService;
        }
        [HttpGet]
        public async Task Verify(string token) =>
            await _LoginTokenService.VerifyJwtToken(token);
    }
}
