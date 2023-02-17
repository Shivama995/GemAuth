using AutoMapper;
using Common.Application;
using Common.Enums;
using Data.Config.DataAccess;
using Data.Config.Models;
using Data.User.DataAccess;
using Data.User.Models;

namespace Application.User.Services.Implementations
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository       _UserRepository;
        private readonly IConfigUserRepository _ConfigUserRepository;

        public UserService(IUserRepository userRepository,
            IConfigUserRepository configUserRepository,
            IMapper mapper) : base(mapper)
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
            ConfigUserDetails ConfigUserDetails = _Mapper.Map<ConfigUserDetails>(user.UserDetails);
            ConfigUserDetails.DBName  = user.OrgDetails.DBName;
            ConfigUserDetails.OrgCode = user.OrgDetails.OrgCode;

            await _ConfigUserRepository.AddUser(ConfigUserDetails);
        }
    }
}
