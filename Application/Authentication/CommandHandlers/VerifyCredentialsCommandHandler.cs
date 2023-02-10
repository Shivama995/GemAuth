using Application.Authentication.Requests;
using Application.Authentication.Services;
using Common.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Authentication.CommandHandlers
{
    public class VerifyCredentialsCommand : IRequest<VerifyCredentialsDTO>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
    public class VerifyCredentialsCommandHandler : IRequestHandler<VerifyCredentialsCommand, VerifyCredentialsDTO>
    {
        private readonly IVerifyCredentialsService _VerifyCredentialsService;

        public VerifyCredentialsCommandHandler(IVerifyCredentialsService verifyCredentialsService)
        {
            _VerifyCredentialsService = verifyCredentialsService;
        }


        public async Task<VerifyCredentialsDTO> Handle(VerifyCredentialsCommand request, CancellationToken cancellationToken)
        {
            return await _VerifyCredentialsService.Verify(request);
        }
    }
}
