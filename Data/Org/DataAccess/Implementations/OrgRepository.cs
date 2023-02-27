using Common.Configuration;
using Common.Data;
using Common.Extensions;
using Data.Config.Models;
using Data.Org.Models;
using MongoDB.Driver;

namespace Data.Org.DataAccess.Implementations
{
    public class OrgRepository : RepoBase, IOrgRepository
    {
        public OrgRepository(IConfigManager configManager) : base(configManager) { }

        public async Task<OrgDetails> GetOrgDetails(string DBName = null)
        {
            if(DBName.HasValue())
                LoadDatabase(DBName);
            var Collection = Database.GetCollection<OrgDetails>("org_base");
            return (await Collection.FindAsync(Builders<OrgDetails>
                .Filter
                .Eq("DBName", DBName)))
                .FirstOrDefault();

        }
        public async Task<OrgDetails> Register(OrgDetails orgDetails, string DBName = null)
        {
            if (DBName.HasValue())
                LoadDatabase(DBName);
            SetModificationDetails(orgDetails);
            var Collection = Database.GetCollection<OrgDetails>("org_base");
            await Collection.InsertOneAsync(orgDetails);
            return orgDetails;
        }

        public async Task<List<string>> GetOrgNames(string DBName = null)
        {
            if(DBName.HasValue())
                LoadDatabase(DBName);
            var Collection = Database.GetCollection<OrgDetails>("org_base");

            return await Collection
                .Find(x => true)
                .Project(row => row.OrgName)
                .ToListAsync();
        }

        #region Private Methods
        private async Task SetUpOrgDetails(OrgDetails orgData, string DBName = null)
        {
            if (DBName.HasValue())
                LoadDatabase(DBName);

            var Collection = Database.GetCollection<OrgDetails>("org_base");
            SetModificationDetails(orgData);

            await Collection.InsertOneAsync(orgData);
        }
        private void SetModificationDetails(OrgDetails orgDetails)
        {
            orgDetails.CreatedOn  = DateTime.Now;
            orgDetails.ModifiedOn = DateTime.Now;
        }
        #endregion
    }
}
