using Application.Authentication.CommandHandlers;
using Application.Authentication.DTOs;
using Application.Token.Services;
using Application.User.Services;
using AutoMapper;
using Common.Application;
using Common.Constants;
using Common.Exceptions;
using Common.Extensions;

namespace Application.Authentication.Services.Implementations
{
    public class VerifyCredentialsService : ServiceBase, IVerifyCredentialsService
    {
        private readonly IUserService    _UserService;
        private readonly ILoginTokenService _LoginTokenService;
        public VerifyCredentialsService(IUserService userService,
            ILoginTokenService loginTokenService,
            IMapper mapper) : base(mapper)
        {
            _UserService    = userService;
            _LoginTokenService = loginTokenService;
        }
        public async Task<VerifyCredentialsDTO> Verify(VerifyCredentialsCommand request)
        {
            var UserAuthData = await _UserService.GetUserAuthData(UserIdentifiers.EmailAddress, request.EmailAddress);
            if (UserAuthData.IsNull())
                throw new UserNotFoundException("Email Address not found!");

            if (!string.Equals(UserAuthData.UserDetails.Password, request.Password))
                throw new UserNotFoundException("Password Incorrect");

            var Token = await _LoginTokenService.CreateLoginToken(UserAuthData.UserDetails);

            return new VerifyCredentialsDTO
            {
                Token     = Token,
                FirstName = UserAuthData.UserDetails.FirstName
            };
        }
    }
}
