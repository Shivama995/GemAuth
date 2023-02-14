using Data.Org.Models;

namespace Data.Org.DataAccess
{
    public interface IOrgRepository
    {
        Task<OrgModel> Register(OrgModel orgData);
        Task<List<string>> GetOrgNames();
    }
}
