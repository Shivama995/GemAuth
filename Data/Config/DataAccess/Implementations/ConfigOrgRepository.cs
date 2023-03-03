using Common.Configuration;
using Data.Config.Models;
using Data.Org.Models;

namespace Data.Config.DataAccess.Implementations
{
    public class ConfigOrgRepository : RepoBase, IConfigOrgRepository
    {
        public ConfigOrgRepository(IConfigManager configManager) : base(configManager) { }
        public async Task SetUpConfigOrg(ConfigOrgDetails configOrgDetails)
        {
            var Collection = Database.GetCollection<ConfigOrgDetails>("org_base");
            SetModificationDetails(configOrgDetails);

            await Collection.InsertOneAsync(configOrgDetails);
        }

        private void SetModificationDetails(ConfigOrgDetails configOrgDetails)
        {
            configOrgDetails.CreatedOn = DateTime.Now;
            configOrgDetails.ModifiedOn = DateTime.Now;
        }
    }
}
