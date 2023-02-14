using Common.Configuration;
using Common.Data;
using Common.Enums;
using Data.User.DataAccess;
using Data.User.Models;

namespace Application.User.Services.Implementations
{
    public class UserService : RepoBase, IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository userRepository,
            IConfigManager configManager) : base(configManager)
        {
            _UserRepository = userRepository;
        }

        public async Task<UserDetailsModel> CreateAdmin(UserAggregateModel user)
        {
            user.Id   = Guid.NewGuid().ToString();
            user.Role = UserRole.Admin;
            
            return await _UserRepository.AddUser(user);
        }
    }
}
