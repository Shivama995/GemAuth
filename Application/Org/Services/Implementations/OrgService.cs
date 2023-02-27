using Application.Org.CommandHandlers;
using Application.Org.DTOs;
using Application.User.Services;
using AutoMapper;
using Common.Application;
using Common.Exceptions;
using Data.Config.DataAccess.Implementations;
using Data.Config.Models;
using Data.Org.DataAccess;
using Data.Org.Models;
using Data.User.Models;

namespace Application.Org.Services.Implementations
{
    public class OrgService : ServiceBase, IOrgService
    {
        #region Private Members
        private readonly IOrgRepository       _OrgRepository;
        private readonly IConfigOrgRepository _ConfigOrgRepository;
        private readonly IUserService         _UserService;
        #endregion

        #region Constructor
        public OrgService(IOrgRepository orgRepository,
            IConfigOrgRepository configOrgRepository,
            IUserService userService,
            IMapper mapper) : base(mapper)
        {
            _OrgRepository       = orgRepository;
            _ConfigOrgRepository = configOrgRepository;
            _UserService         = userService;
        }
        #endregion

        #region Public Methods
        public async Task<RegisterAppDTO> Register(RegisterAppCommand request)
        {
            await ValidateOrgData(request);
            var OrgData = SetOrgDetails(request);
            //Filling OrgData with all details
            await RegisterNewOrg(OrgData);
            await CreateAdminForOrg(request, OrgData);
            return _Mapper.Map<RegisterAppDTO>(OrgData);
        }

        #endregion

        #region Private Methods
        private async Task<UserDetailsModel> CreateAdminForOrg(RegisterAppCommand request, OrgDetails orgDetails)
        {
            return await _UserService.CreateAdmin(new UserAggregateModel
            {
               UserDetails      = new UserDetailsModel
               {
                   FirstName    = request.FirstName,
                   LastName     = request.LastName,
                   EmailAddress = request.EmailAddress,
                   Password     = request.Password
               },
               OrgDetails       = orgDetails
            });
         }
        private async Task ValidateOrgData(RegisterAppCommand request)
        {
           var OrgNames = await _OrgRepository.GetOrgNames();
            if (OrgNames.Contains(request.OrgName))
                throw new AppAlreadyExistsException();
        }
        private OrgDetails SetOrgDetails(RegisterAppCommand request)
        {
            return new OrgDetails
            {
                OrgName = request.OrgName,
                OrgCode = Guid.NewGuid().ToString(),
                DBName  = "gemini_" + request.OrgName.ToLower()
            };
        }
        private async Task RegisterNewOrg(OrgDetails orgData)
        {
            //Setup Org in Config DB
            await _ConfigOrgRepository.SetUpConfigOrg(
                _Mapper.Map<ConfigOrgDetails>(orgData));

            //Setup Org
            await _OrgRepository.Register(orgData, orgData.DBName);
        }
        #endregion
    }
}
