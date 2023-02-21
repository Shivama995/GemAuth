using Application.Authentication.CommandHandlers;
using Application.Authentication.Requests;
using Application.Token.Services;
using Application.User.Services;
using AutoMapper;
using Common.Application;
using Common.Exceptions;
using Common.Extensions;
using Data.User.DataAccess;

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
            var UserAggregateData = await _UserService.GetUserAggregateData(request.EmailAddress);
            if (UserAggregateData.IsNull())
                throw new UserNotFoundException("Email Address not found!");

            if (!string.Equals(UserAggregateData.UserDetails.Password, request.Password))
                throw new UserNotFoundException("Password Incorrect");

            var Token = await _LoginTokenService.CreateLoginToken(UserAggregateData.UserDetails);

            return new VerifyCredentialsDTO
            {
                Token     = Token,
                FirstName = UserAggregateData.UserDetails.FirstName
            };
        }
    }
}
