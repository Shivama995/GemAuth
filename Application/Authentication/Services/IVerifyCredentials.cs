using Application.Authentication.CommandHandlers;
using Application.Authentication.Requests;

namespace Application.Authentication.Services
{
    public interface IVerifyCredentials
    {
        public Task<VerifyCredentialsDTO> Verify(VerifyCredentialsCommand request);
    }
}
