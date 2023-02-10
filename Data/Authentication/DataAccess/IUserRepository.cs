using Data.Authentication.Models;

namespace Data.Authentication.DataAccess
{
    public interface IUserRepository
    {
        Task<UserDetailsModel> GetUserDetails(string emailAddress);
    }
}
