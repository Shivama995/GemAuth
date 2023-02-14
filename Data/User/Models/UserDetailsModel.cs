using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Common.Enums;

namespace Data.User.Models
{ 
    [BsonIgnoreExtraElements]
    public class UserDetailsModel
    {
        [BsonId]
        public ObjectId _id          { get; set; }
        public string   Id           { get; set; }
        public string   FirstName    { get; set; }
        public string   LastName     { get; set; }
        public string   EmailAddress { get; set; }
        public string   Password     { get; set; }
        public string   OrgCode      { get; set; }
        public UserRole Role         { get; set; }
    }
}