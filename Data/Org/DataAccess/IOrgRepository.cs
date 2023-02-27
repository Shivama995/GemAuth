using Data.Org.Models;

namespace Data.Org.DataAccess
{
    public interface IOrgRepository
    {
        Task<OrgDetails> Register(OrgDetails orgDetails, string DBName = null);
        Task<List<string>> GetOrgNames(string DBName = null);
        Task<OrgDetails> GetOrgDetails(string DBName = null);
    }
}
