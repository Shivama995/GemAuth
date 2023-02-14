using Common.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.User.Models
{
    public class UserAggregateModel
    {
        [BsonId]
        public ObjectId _id         { get; set; }
        public string Id            { get; set; }
        public string FirstName     { get; set; }
        public string LastName      { get; set; }
        public string EmailAddress  { get; set; }
        public string Password      { get; set; }
        public string OrgCode       { get; set; }
        public string DBName        { get; set; }
        public UserRole Role        { get; set; }
    }
}
