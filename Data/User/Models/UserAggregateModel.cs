using Common.Enums;
using Data.Org.Models;
using MongoDB.Bson;

namespace Data.User.Models
{
    public class UserAggregateModel
    {
        public OrgDetails       OrgDetails { get; set; }
        public UserDetailsModel UserDetails { get; set; }
    }
}
