using Application.User.DTOs;
using AutoMapper;
using Data.User.Models;

namespace Application.User.Mappings
{
    public class AddUserDTOMapper : Profile
    {
        public AddUserDTOMapper()
        {
            CreateMap<UserDetailsModel, AddUserDTO>();
        }
    }
}
