using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Authentication.Services
{
    public interface IHandleAuthenticationService
    {
        Task HandleAuthentication(AuthorizationFilterContext context);
    }
}
