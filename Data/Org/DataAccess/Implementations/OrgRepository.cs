using Common.Configuration;
using Common.Data;
using Data.Org.Models;
using MongoDB.Driver;

namespace Data.Org.DataAccess.Implementations
{
    public class OrgRepository : RepoBase, IOrgRepository
    {
        public OrgRepository(IConfigManager configManager) : base(configManager) { }

        public async Task<OrgModel> Register(OrgModel orgData)
        {
            var Collection = Database.GetCollection<OrgModel>("org_base");
            SetModificationDetails(orgData);

            await Collection.InsertOneAsync(orgData);
            return orgData;
        }

        public async Task<List<string>> GetOrgNames()
        {
            var Collection = Database.GetCollection<OrgModel>("org_base");

            return await Collection
                .Find(x => true)
                .Project(row => row.OrgName)
                .ToListAsync();
        }

        private void SetModificationDetails(OrgModel orgData)
        {
            orgData.CreatedOn  = DateTime.Now;
            orgData.ModifiedOn = DateTime.Now;
        }
    }
}
