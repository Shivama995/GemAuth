using Data.Org.Models;

namespace Data.Org.DataAccess
{
    public interface IOrgRepository
    {
        Task<OrgDetails> Register(OrgDetails orgDetails, string DBName = null);
        Task<List<string>> GetOrgNames();
        Task<OrgDetails> GetOrgDetails();
        Task<OrgDetails> GetOrgDetailsFromDB(string DBName = null);
    }
}
