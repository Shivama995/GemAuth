using Application.Org.DTOs;
using AutoMapper;
using Data.Config.Models;
using Data.Org.Models;

namespace Application.Org.Mappings
{
    public class ConfigOrgDetailsMapper : Profile
    {
        public ConfigOrgDetailsMapper()
        {
            CreateMap<OrgDetails, ConfigOrgDetails>();
        }
    }
}
