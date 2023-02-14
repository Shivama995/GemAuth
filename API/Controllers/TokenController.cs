using Application.Token.DTOs;
using Application.Token.QueryHandler;
using Application.Token.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TokenController : BaseController
    {
        ILoginTokenService _LoginTokenService;
        public TokenController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }
        [HttpGet]
        [Route("Verify")]
        public async Task<VerifyLoginTokenDTO> Verify(string token) =>
            await QueryAsync(new VerifyLoginTokenQuery { Token = token });
    }
}
