using Data.Config.Models;
using Data.Org.Models;

namespace Data.Config.DataAccess.Implementations
{
    public interface IConfigOrgRepository
    {
        Task SetUpConfigOrg(ConfigOrgDetails configOrgDetails);
    }
}
