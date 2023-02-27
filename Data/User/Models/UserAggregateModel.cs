using Data.Org.Models;

namespace Data.User.Models
{
    public class UserAggregateModel
    {
        public OrgDetails       OrgDetails { get; set; }
        public UserDetailsModel UserDetails { get; set; }
    }
}
