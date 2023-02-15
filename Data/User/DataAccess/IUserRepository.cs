using Data.User.Models;

namespace Data.User.DataAccess
{
    public interface IUserRepository
    {
        Task<UserDetailsModel> GetUserDetails(string emailAddress);
        Task<UserDetailsModel> AddAdmin(UserDetailsModel user, string DBName);
    }
}
