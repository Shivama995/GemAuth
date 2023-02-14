using Data.User.Models;

namespace Application.User.Services
{
    public interface IUserService
    {
        public Task<UserDetailsModel> CreateAdmin(UserAggregateModel user);
    }
}
