using Common.Configuration;
using Common.Data;
using Common.Enums;
using Data.Config.DataAccess;
using Data.User.DataAccess;
using Data.User.Models;

namespace Application.User.Services.Implementations
{
    public class UserService : RepoBase, IUserService
    {
        private readonly IUserRepository       _UserRepository;
        private readonly IConfigUserRepository _ConfigUserRepository;

        public UserService(IUserRepository userRepository,
            IConfigManager configManager,
            IConfigUserRepository configUserRepository) : base(configManager)
        {
            _UserRepository       = userRepository;
            _ConfigUserRepository = configUserRepository;
        }

        public async Task<UserDetailsModel> CreateAdmin(UserAggregateModel user)
        {
            user.UserDetails.Id   = Guid.NewGuid().ToString();
            user.UserDetails.Role = UserRole.Admin;

            await AddAdminForConfig(user);
            return await _UserRepository.AddAdmin(user.UserDetails, user.OrgDetails.DBName);
        }

        private async Task AddAdminForConfig(UserAggregateModel user)
        {
            await _ConfigUserRepository.AddUser(new Data.Config.Models.ConfigUserDetails
            {
                Id           = user.UserDetails.Id,
                FirstName    = user.UserDetails.FirstName,
                LastName     = user.UserDetails.LastName,
                EmailAddress = user.UserDetails.EmailAddress,
                Role         = user.UserDetails.Role,
                DBName       = user.OrgDetails.DBName,
                OrgCode      = user.OrgDetails.OrgCode,
            });
        }
    }
}
