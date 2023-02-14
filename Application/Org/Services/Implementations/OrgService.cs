using Application.Org.CommandHandlers;
using Application.Org.DTOs;
using Application.User.Services;
using Common.Application;
using Common.Exceptions;
using Data.Org.DataAccess;
using Data.Org.Models;
using Data.User.Models;

namespace Application.Org.Services.Implementations
{
    public class OrgService : ServiceBase, IOrgService
    {
        #region Private Members
        private readonly IOrgRepository _OrgRepository;
        private readonly IUserService   _UserService;
        #endregion

        #region Constructor
        public OrgService(IOrgRepository orgRepository,
            IUserService userService)
        {
            _OrgRepository = orgRepository;
            _UserService   = userService;
        }
        #endregion

        #region Public Methods
        public async Task<RegisterAppDTO> Register(RegisterAppCommand request)
        {
            await ValidateOrgData(request);
            var OrgData = SetOrgData(request);
            //Filling OrgData with all details
            OrgData = await _OrgRepository.Register(OrgData);
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
        private async Task<UserDetailsModel> CreateAdminForOrg(RegisterAppCommand request, OrgModel orgData)
        {
            return await _UserService.CreateAdmin(new UserAggregateModel
            {
                FirstName    = request.FirstName,
                LastName     = request.LastName,
                EmailAddress = request.EmailAddress,
                Password     = request.Password,
                OrgCode      = orgData.OrgCode,
                DBName       = orgData.DBName
            });
        }
        private async Task ValidateOrgData(RegisterAppCommand request)
        {
           var OrgNames = await _OrgRepository.GetOrgNames();
            if (OrgNames.Contains(request.OrgName))
                throw new AppAlreadyExistsException();
        }
        private OrgModel SetOrgData(RegisterAppCommand request)
        {
            return new OrgModel
            {
                OrgName = request.OrgName,
                OrgCode = Guid.NewGuid().ToString(),
                DBName  = "gemini_" + request.OrgName.ToLower()
            };
        }
        #endregion
    }
}
