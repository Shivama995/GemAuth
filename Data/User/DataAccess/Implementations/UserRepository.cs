using Common.Configuration;
using Common.Data;
using Data.Config.Models;
using Data.User.Models;
using MongoDB.Driver;

namespace Data.User.DataAccess.Implementations
{
    public class UserRepository : RepoBase, IUserRepository
    {
        public UserRepository(IConfigManager configManager) : base(configManager) { }
        public async Task<UserDetailsModel> GetUserDetails(string emailAddress, string DBName)
        {
            LoadDatabase(DBName);
            var Collection = Database.GetCollection<UserDetailsModel>("user_base");

            return (await Collection.FindAsync(Builders<UserDetailsModel>
                .Filter
                .Eq("EmailAddress", emailAddress)))
                .FirstOrDefault();

        }
        public async Task<UserDetailsModel> AddAdmin(UserDetailsModel user, string DBName)
        {
            LoadDatabase(DBName);
            var Collection = Database.GetCollection<UserDetailsModel>("user_base");
            await Collection.InsertOneAsync(user);
            return user;
        }
    }
}
