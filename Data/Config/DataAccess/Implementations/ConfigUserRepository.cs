using Common.Configuration;
using Data.Config.Models;
using MongoDB.Driver;

namespace Data.Config.DataAccess.Implementations
{
    public class ConfigUserRepository : RepoBase, IConfigUserRepository
    {
        public ConfigUserRepository(IConfigManager configManager) : base(configManager) { }

        public async Task AddUser(ConfigUserDetails configUserDetails)
        {
            var Collection = Database.GetCollection<ConfigUserDetails>("user_base");

            await Collection.InsertOneAsync(configUserDetails);
        }
        public async Task<ConfigUserDetails> GetConfigUserDetails(string identifier, string id)
        {
            var Collection = Database.GetCollection<ConfigUserDetails>("user_base");

            return (await Collection.FindAsync(Builders<ConfigUserDetails>
                .Filter
                .Eq(identifier, id)))
                .FirstOrDefault();
        }
    }
}
