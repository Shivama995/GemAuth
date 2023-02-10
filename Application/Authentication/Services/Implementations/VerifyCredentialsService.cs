using Application.Authentication.CommandHandlers;
using Application.Authentication.Requests;
using Common.Application;
using Common.Exceptions;
using Common.Extensions;
using Data.Authentication.DataAccess;

namespace Application.Authentication.Services.Implementations
{
    public class VerifyCredentialsService : ServiceBase, IVerifyCredentialsService
    {
        private readonly IUserRepository _UserRepository;

        public VerifyCredentialsService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public async Task<VerifyCredentialsDTO> Verify(VerifyCredentialsCommand request)
        {
            var UserDetails = await _UserRepository.GetUserDetails(request.EmailAddress);
            if (UserDetails.IsNull())
                throw new UserNotFoundException("Email Address not found!");

            if (!string.Equals(UserDetails.Password, request.Password))
                throw new UserNotFoundException("Password Incorrect");


            return new VerifyCredentialsDTO
            {
                FirstName = UserDetails.FirstName
            };
        }
    }
}
