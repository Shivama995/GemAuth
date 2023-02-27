using Application.Authentication.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Authentication.Attributes
{
    public class APIAuthHandleFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly IHandleAuthenticationService _HandleAuthenticationService;

        public APIAuthHandleFilter(IHandleAuthenticationService handleAuthenticationService)
        {
            _HandleAuthenticationService = handleAuthenticationService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _HandleAuthenticationService.HandleAuthentication(context).Wait();
        }
    }
}
