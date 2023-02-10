using Application.Authentication.Requests;
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
        public VerifyCredentialsCommandHandler() { }
        public async Task<VerifyCredentialsDTO> Handle(VerifyCredentialsCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            throw new Exception();
        }
    }
}
