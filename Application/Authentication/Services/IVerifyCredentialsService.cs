using Application.Authentication.CommandHandlers;
using Application.Authentication.Requests;

namespace Application.Authentication.Services
{
    public interface IVerifyCredentialsService
    {
        public Task<VerifyCredentialsDTO> Verify(VerifyCredentialsCommand request);
    }
}
