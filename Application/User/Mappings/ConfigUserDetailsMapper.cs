using AutoMapper;
using Data.Config.Models;
using Data.User.Models;

namespace Application.User.Mappings
{
    public class ConfigUserDetailsMapper : Profile
    {
        public ConfigUserDetailsMapper()
        {
            CreateMap<UserDetailsModel, ConfigUserDetails>();
        }
    }
}
