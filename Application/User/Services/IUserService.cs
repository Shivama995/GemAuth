using Data.User.Models;

namespace Application.User.Services
{
    public interface IUserService
    {
        public Task<UserDetailsModel> CreateAdmin(UserAggregateModel user);
        Task<UserAggregateModel> GetUserAggregateData(string emailAddress);
    }
}
