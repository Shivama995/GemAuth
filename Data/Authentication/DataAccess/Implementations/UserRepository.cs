using Common.Configuration;
using Common.Constants;
using Common.Data;
using Data.Authentication.Models;
using MongoDB.Driver;

namespace Data.Authentication.DataAccess.Implementations
{
    public class UserRepository : RepoBase, IUserRepository
    {
        public UserRepository(IConfigManager configManager) : base(configManager) { }
        public async Task<UserDetailsModel> GetUserDetails(string emailAddress)
        {
            var Collection = Database.GetCollection<UserDetailsModel>("user_base");

            return (await Collection.FindAsync(Builders<UserDetailsModel>
                .Filter
                .Eq("EmailAddress", emailAddress)))
                .FirstOrDefault();

        }
    }
}
