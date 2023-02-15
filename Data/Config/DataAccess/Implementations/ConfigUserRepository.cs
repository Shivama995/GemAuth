using Common.Configuration;
using Common.Data;
using Data.Config.Models;

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
    }
}
