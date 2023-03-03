using Data.Org.Models;

namespace Data.User.Models
{
    public static class UserAggregateAuthModel
    {
        public static UserDetailsModel UserDetails { get; set; }
        public static OrgDetails       OrgDetails       { get; set; }
    }
}
