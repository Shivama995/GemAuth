using Data.Org.Models;
using Data.User.Models;

namespace Application.Authentication
{
    public static class UserAggregateAuthModel
    {
        public static OrgDetails       OrgDetails { get; set; }
        public static UserDetailsModel UserDetails { get; set; }
    }
}
