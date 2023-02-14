using Application.Token.DTOs;
using Application.Token.Services;
using MediatR;

namespace Application.Token.QueryHandler
{
    public class VerifyLoginTokenQuery : IRequest<VerifyLoginTokenDTO>
    {
        public string Token { get; set; }
    }
    public class VerifyLoginTokenQueryHandler : IRequestHandler<VerifyLoginTokenQuery, VerifyLoginTokenDTO>
    {
        private readonly ILoginTokenService _LoginTokenService;

        public VerifyLoginTokenQueryHandler(ILoginTokenService loginTokenService)
        {
            _LoginTokenService = loginTokenService;
        }
        public async Task<VerifyLoginTokenDTO> Handle(VerifyLoginTokenQuery request, CancellationToken cancellationToken) =>
            await _LoginTokenService.VerifyJwtToken(request.Token);
    }
}
