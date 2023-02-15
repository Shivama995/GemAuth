using Application.Org.CommandHandlers;
using Application.Org.DTOs;
using Application.User.Services;
using Common.Application;
using Common.Exceptions;
using Data.Config.DataAccess.Implementations;
using Data.Org.DataAccess;
using Data.Org.Models;
using Data.User.Models;

namespace Application.Org.Services.Implementations
{
    public class OrgService : ServiceBase, IOrgService
    {
        #region Private Members
        private readonly IOrgRepository _OrgRepository;
        private readonly IConfigOrgRepository _ConfigOrgRepository;
        private readonly IUserService   _UserService;
        #endregion

        #region Constructor
        public OrgService(IOrgRepository orgRepository,
            IConfigOrgRepository configOrgRepository,
            IUserService userService)
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
            return new RegisterAppDTO
            {
                OrgCode    = OrgData.OrgCode,
                OrgName    = OrgData.OrgName,
                CreatedOn  = OrgData.CreatedOn,
                ModifiedOn = OrgData.ModifiedOn
            };
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
               OrgDetails  = new OrgDetails
               {
                   OrgCode = orgDetails.OrgCode,
                   OrgName = orgDetails.OrgName,
                   DBName  = orgDetails.DBName
               }
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
            await _ConfigOrgRepository.SetUpConfigOrg(new Data.Config.Models.ConfigOrgDetails
            {
                OrgName = orgData.OrgName,
                OrgCode = orgData.OrgCode,
                DBName  = orgData.DBName
            });

            //Setup Org
            await _OrgRepository.Register(new OrgDetails
            {
                OrgCode = orgData.OrgCode,
                OrgName = orgData.OrgName,
                DBName  = orgData.DBName
            });
        }
        #endregion
    }
}
