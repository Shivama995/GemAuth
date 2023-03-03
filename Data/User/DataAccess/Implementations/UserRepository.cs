using Common.Configuration;
using Common.Extensions;
using Data.User.Models;
using MongoDB.Driver;

namespace Data.User.DataAccess.Implementations
{
    public class UserRepository : RepoBase, IUserRepository
    {
        public UserRepository(IConfigManager configManager) : base(configManager) { }
        public async Task<UserDetailsModel> GetUserDetails(string identifier, string id)
        {
            LoadOrgDatabase();
            var Collection = Database.GetCollection<UserDetailsModel>("user_base");

            return (await Collection.FindAsync(Builders<UserDetailsModel>
                .Filter
                .Eq(identifier, id)))
                .FirstOrDefault();

        }
        public async Task<UserDetailsModel> GetUserDetailsFromDB(string identifier, string id, string DBName = null)
        {
            if (DBName.HasValue())
                LoadDatabase(DBName);;
            var Collection = Database.GetCollection<UserDetailsModel>("user_base");

            return (await Collection.FindAsync(Builders<UserDetailsModel>
                .Filter
                .Eq(identifier, id)))
                .FirstOrDefault();

        }
        public async Task<UserDetailsModel> AddNewUser(UserDetailsModel user, string DBName = null)
        {
            if (DBName.HasValue())
                LoadDatabase(DBName);

            var Collection = Database.GetCollection<UserDetailsModel>("user_base");
            await Collection.InsertOneAsync(user);
            return user;
        }
    }
}
