using Application.Token.Services;
using Common.Exceptions;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Application.Authentication.Services.Implementations
{
    public class HandleAuthenticationService : IHandleAuthenticationService
    {
        private readonly ILoginTokenService _LoginTokenService;

        public HandleAuthenticationService(ILoginTokenService loginTokenService)
        {
            _LoginTokenService = loginTokenService;
        }
        public async Task HandleAuthentication(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues AuthHeader);
            if (AuthHeader.IsNullOrEmpty()) throw new AuthorizationTokenMissingException();

            await VerifyAuthorizationToken(AuthHeader);
        }

        public async Task VerifyAuthorizationToken(string token)
        {
            try
            {
                var LoginTokenData = await _LoginTokenService.VerifyJwtToken(token);
                if (LoginTokenData.IsNull()) { throw new InvalidAuthorizationTokenException(); }
            }
            catch (Exception ex) { throw new InvalidAuthorizationTokenException(); }
        }
    }
}
