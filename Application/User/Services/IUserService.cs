using Application.User.DTOs;
using Data.User.Models;

namespace Application.User.Services
{
    public interface IUserService
    {
        public Task<UserDetailsModel> CreateAdmin(UserAggregateModel user);
        Task<UserAggregateModel> GetUserAggregateData(string identifier, string id);
        Task<UserAggregateModel> GetUserAuthData(string identifier, string id);
        Task<GetUserDTO> GetUserData(string identifier, string id);
    }
}
