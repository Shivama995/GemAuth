using Common.Configuration;
using Common.Data;
using Data.Org.Models;
using MongoDB.Driver;

namespace Data.Org.DataAccess.Implementations
{
    public class OrgRepository : RepoBase, IOrgRepository
    {
        public OrgRepository(IConfigManager configManager) : base(configManager) { }

        public async Task<OrgDetails> Register(OrgDetails orgDetails)
        {
            SetModificationDetails(orgDetails);
            LoadDatabase(orgDetails.DBName);

            var Collection = Database.GetCollection<OrgDetails>("org_base");
            await Collection.InsertOneAsync(orgDetails);
            return orgDetails;
        }

        public async Task<List<string>> GetOrgNames()
        {
            var Collection = Database.GetCollection<OrgDetails>("org_base");

            return await Collection
                .Find(x => true)
                .Project(row => row.OrgName)
                .ToListAsync();
        }
        private async Task SetUpOrgDetails(OrgDetails orgData)
        {
            LoadDatabase(orgData.DBName);

            var Collection = Database.GetCollection<OrgDetails>("org_base");

            await Collection.InsertOneAsync(
                new OrgDetails
                {
                    OrgName    = orgData.OrgName,
                    OrgCode    = orgData.OrgCode,
                    DBName     = orgData.DBName,
                    ModifiedOn = DateTime.Now,
                    CreatedOn  = DateTime.Now,
                });
        }
        private void SetModificationDetails(OrgDetails orgDetails)
        {
            orgDetails.CreatedOn  = DateTime.Now;
            orgDetails.ModifiedOn = DateTime.Now;
        }
    }
}
