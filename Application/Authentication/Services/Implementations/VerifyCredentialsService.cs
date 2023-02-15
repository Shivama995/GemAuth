using Application.Authentication.CommandHandlers;
using Application.Authentication.Requests;
using Application.Token.Services;
using Common.Application;
using Common.Exceptions;
using Common.Extensions;
using Data.User.DataAccess;

namespace Application.Authentication.Services.Implementations
{
    public class VerifyCredentialsService : ServiceBase, IVerifyCredentialsService
    {
        private readonly IUserRepository    _UserRepository;
        private readonly ILoginTokenService _LoginTokenService;
        public VerifyCredentialsService(IUserRepository userRepository,
            ILoginTokenService loginTokenService)
        {
            _UserRepository    = userRepository;
            _LoginTokenService = loginTokenService;
        }
        public async Task<VerifyCredentialsDTO> Verify(VerifyCredentialsCommand request)
        {
            var UserDetails = await _UserRepository.GetUserDetails(request.EmailAddress);
            if (UserDetails.IsNull())
                throw new UserNotFoundException("Email Address not found!");

            if (!string.Equals(UserDetails.Password, request.Password))
                throw new UserNotFoundException("Password Incorrect");

            var Token = await _LoginTokenService.CreateLoginToken(UserDetails);

            return new VerifyCredentialsDTO
            {
                Token     = Token,
                FirstName = UserDetails.FirstName
            };
        }
    }
}
