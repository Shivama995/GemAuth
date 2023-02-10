using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IMediator _MediatR;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
            _MediatR = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IMediator>();
        }
        public async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await _MediatR.Send(command);
        }
        public async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            return await _MediatR.Send(query);
        }
    }
}
