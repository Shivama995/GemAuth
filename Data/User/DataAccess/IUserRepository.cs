using Data.Config.Models;
using Data.User.Models;

namespace Data.User.DataAccess
{
    public interface IUserRepository
    {
        Task<UserDetailsModel> GetUserDetails(string emailAddress, string DBName);
        Task<UserDetailsModel> AddAdmin(UserDetailsModel user, string DBName);
    }
}
