using Common.Configuration;
using Common.Data;
using Data.Config.Models;
using Data.Org.Models;
using MongoDB.Driver;

namespace Data.Org.DataAccess.Implementations
{
    public class OrgRepository : RepoBase, IOrgRepository
    {
        public OrgRepository(IConfigManager configManager) : base(configManager) { }

        public async Task<OrgDetails> GetOrgDetails(string DBName)
        {
            LoadDatabase(DBName);
            var Collection = Database.GetCollection<OrgDetails>("org_base");
            return (await Collection.FindAsync(Builders<OrgDetails>
                .Filter
                .Eq("DBName", DBName)))
                .FirstOrDefault();

        }
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
            SetModificationDetails(orgData);

            await Collection.InsertOneAsync(orgData);
        }
        private void SetModificationDetails(OrgDetails orgDetails)
        {
            orgDetails.CreatedOn  = DateTime.Now;
            orgDetails.ModifiedOn = DateTime.Now;
        }
    }
}
