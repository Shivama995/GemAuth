using Data.Config.Models;

namespace Data.Config.DataAccess
{
    public interface IConfigUserRepository
    {
        Task AddUser(ConfigUserDetails configUserDetails);
        Task<ConfigUserDetails> GetConfigUserDetails(string identifier, string id);
    }
}
