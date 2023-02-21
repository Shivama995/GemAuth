using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Filters;

namespace Application.Authentication.Attributes
{
    public class ApiAuthorizationAttribute : TypeFilterAttribute
    {
        public ApiAuthorizationAttribute() : base(typeof(APIAuthHandleFilter)) { }
    }
}
