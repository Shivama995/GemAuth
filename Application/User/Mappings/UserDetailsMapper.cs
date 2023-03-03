using Application.User.CommandHandlers;
using AutoMapper;
using Data.User.Models;

namespace Application.User.Mappings
{
    public class UserDetailsMapper : Profile
    {
        public UserDetailsMapper()
        {
            CreateMap<AddUserCommand, UserDetailsModel>();
        }
    }
}
