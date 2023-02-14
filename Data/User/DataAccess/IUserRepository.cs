using Data.User.Models;

namespace Data.User.DataAccess
{
    public interface IUserRepository
    {
        Task<UserDetailsModel> GetUserDetails(string emailAddress);
        Task<UserDetailsModel> AddUser(UserAggregateModel user);
    }
}
