using Application.Authentication.Requests;
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
        private readonly IVerifyCredentials _VerifyCredentials;

        public VerifyCredentialsCommandHandler(IVerifyCredentials verifyCredentials)
        {
            _VerifyCredentials = verifyCredentials;
        }
        public async Task<VerifyCredentialsDTO> Handle(VerifyCredentialsCommand request, CancellationToken cancellationToken)
        {
            return await _VerifyCredentials.Verify(request);
        }
    }
}
