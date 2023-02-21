using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Data.Org.Models
{
    public class OrgDetails
    {
        [BsonId]
        public ObjectId _id        { get; set; }
        public string OrgName      { get; set; }
        public string OrgCode      { get; set; }
        public string DBName       { get; set; }
        public DateTime CreatedOn  { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
