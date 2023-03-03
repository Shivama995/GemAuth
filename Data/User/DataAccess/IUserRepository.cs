using Data.User.Models;

namespace Data.User.DataAccess
{
    public interface IUserRepository
    {
        Task<UserDetailsModel> GetUserDetails(string identifier, string id);
        Task<UserDetailsModel> GetUserDetailsFromDB(string identifier, string id, string DBName = null);
        Task<UserDetailsModel> AddAdmin(UserDetailsModel user, string DBName = null);
    }
}
