using Application.Authentication.DTOs;
using Application.Authentication.Services;
using MediatR;

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


        public async Task<VerifyCredentialsDTO> Handle(VerifyCredentialsCommand request, CancellationToken cancellationToken) =>
            await _VerifyCredentialsService.Verify(request);
    }
}
