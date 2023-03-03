using Application.Authentication.CommandHandlers;
using Application.Authentication.DTOs;

namespace Application.Authentication.Services
{
    public interface IVerifyCredentialsService
    {
        public Task<VerifyCredentialsDTO> Verify(VerifyCredentialsCommand request);
    }
}
