using Application.User.CommandHandlers;
using Application.User.DTOs;
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
        public async Task<UserAggregateModel> GetUserAggregateData(string identifier, string id)
        {
            var ConfigUserDetails = await _ConfigUserRepository.GetConfigUserDetails(identifier, id);

            if (ConfigUserDetails.IsNull())
                throw new UserNotFoundException("User Not Found!");

            var OrgDetails        = await _OrgRepository.GetOrgDetails();
            var UserDetails       = await _UserRepository.GetUserDetails(identifier, id);

            return new UserAggregateModel { OrgDetails = OrgDetails, UserDetails = UserDetails };
        }
        public async Task<UserAggregateModel> GetUserAuthData(string identifier, string id)
        {
            var ConfigUserDetails = await _ConfigUserRepository.GetConfigUserDetails(identifier, id);

            if (ConfigUserDetails.IsNull())
                throw new UserNotFoundException("User Not Found!");

            var OrgDetails        = await _OrgRepository.GetOrgDetailsFromDB(ConfigUserDetails.DBName);
            var UserDetails       = await _UserRepository.GetUserDetailsFromDB(identifier, id, OrgDetails.DBName);

            return new UserAggregateModel { OrgDetails = OrgDetails, UserDetails = UserDetails };
        }
        public async Task<GetUserDTO> GetUserData(string identifier, string id)
        {
            var UserData = (await GetUserAggregateData(identifier, id)).UserDetails;
            return _Mapper.Map<GetUserDTO>(UserData);
        }
        public async Task<UserDetailsModel> CreateAdmin(UserAggregateModel user)
        {
            user.UserDetails.Id   = Guid.NewGuid().ToString();
            user.UserDetails.Role = UserRole.Admin;

            await AddUserForConfig(user);
            return await _UserRepository.AddNewUser(user.UserDetails, user.OrgDetails.DBName);
        }
        public async Task<AddUserDTO> CreateNewUser(AddUserCommand user)
        {
            ValidateCreateNewUserRequirements();
            UserDetailsModel User = new();
            User                  = _Mapper.Map<UserDetailsModel>(user);
            User.Id               = Guid.NewGuid().ToString();
            User.OrgCode          = UserAggregateAuthModel.OrgDetails.OrgCode;
            User.Role             = (UserRole)Enum.Parse(typeof(UserRole), user.Role);

            await AddUserForConfig(new UserAggregateModel { UserDetails = User, OrgDetails = UserAggregateAuthModel.OrgDetails });
            var NewUser = await _UserRepository.AddNewUser(User, UserAggregateAuthModel.OrgDetails.DBName);
            return _Mapper.Map<AddUserDTO>(NewUser);
        }

        private void ValidateCreateNewUserRequirements()
        {
            if (UserAggregateAuthModel.UserDetails.IsNull() || UserAggregateAuthModel.UserDetails.Role != UserRole.Admin)
                throw new UnauthorizedAccessException();
        }

        private async Task AddUserForConfig(UserAggregateModel user)
        {
            ConfigUserDetails ConfigUserDetails = _Mapper.Map<ConfigUserDetails>(user.UserDetails);
            ConfigUserDetails.DBName  = user.OrgDetails.DBName;
            ConfigUserDetails.OrgCode = user.OrgDetails.OrgCode;

            await _ConfigUserRepository.AddUser(ConfigUserDetails);
        }
    }
}
