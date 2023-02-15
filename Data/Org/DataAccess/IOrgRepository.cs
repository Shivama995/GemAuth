using Data.Org.Models;

namespace Data.Org.DataAccess
{
    public interface IOrgRepository
    {
        Task<OrgDetails> Register(OrgDetails orgDetails);
        Task<List<string>> GetOrgNames();
    }
}
