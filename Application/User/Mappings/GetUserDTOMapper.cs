using Application.User.DTOs;
using AutoMapper;
using Data.User.Models;

namespace Application.User.Mappings
{
    public class GetUserDTOMapper : Profile
    {
        public GetUserDTOMapper()
        {
            CreateMap<UserDetailsModel, GetUserDTO>();
        }
    }
}
