using Application.Authentication.CommandHandlers;
using Application.Authentication.Requests;
using Data.Authentication.DataAccess;

namespace Application.Authentication.Services.Implementations
{
    public class VerifyCredentials : IVerifyCredentials
    {
        private readonly IUserStore _UserStore;

        public VerifyCredentials(IUserStore userStore)
        {
            _UserStore = userStore;
        }
        public async Task<VerifyCredentialsDTO> Verify(VerifyCredentialsCommand request)
        {
            await Task.CompletedTask;
            throw new Exception();
        }
    }
}
