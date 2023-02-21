using AutoMapper;
using Common.Application;
using Common.Enums;
using Common.Exceptions;
using Common.Extensions;
using Data.Config.DataAccess;
using Data.Config.Models;
using Data.Org.DataAccess;
using Data.User.DataAccess;
using Data.User.Models;

namespace Application.User.Services.Implementations
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository       _UserRepository;
        private readonly IConfigUserRepository _ConfigUserRepository;
        private readonly IOrgRepository        _OrgRepository;

        public UserService(IUserRepository userRepository,
            IConfigUserRepository configUserRepository,
            IOrgRepository orgRepository,
            IMapper mapper) : base(mapper)
        {
            _UserRepository       = userRepository;
            _ConfigUserRepository = configUserRepository;
            _OrgRepository        = orgRepository;
        }
        public async Task<UserAggregateModel> GetUserAggregateData(string emailAddress)
        {
            var ConfigUserDetails = await _ConfigUserRepository.GetConfigUserDetails(emailAddress);

            if (ConfigUserDetails.IsNull())
                throw new UserNotFoundException("User Not Found!");

            var OrgDetails        = await _OrgRepository.GetOrgDetails(ConfigUserDetails.DBName);
            var UserDetails       = await _UserRepository.GetUserDetails(emailAddress, OrgDetails.DBName);

            return new UserAggregateModel { OrgDetails = OrgDetails, UserDetails = UserDetails };
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
