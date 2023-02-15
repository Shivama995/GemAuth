using Common.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Data.Config.Models
{
    [BsonIgnoreExtraElements]
    public class ConfigUserDetails
    {
        [BsonId]
        public ObjectId _id        { get; set; }
        public string Id           { get; set; }
        public string FirstName    { get; set; }
        public string LastName     { get; set; }
        public string EmailAddress { get; set; }
        public string OrgCode      { get; set; }
        public string DBName       { get; set; }
        public UserRole Role       { get; set; }
    }
}
