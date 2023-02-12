using Common.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Common.Data
{
    public class RepoBase
    {
        public readonly string              ConnectionString;
        public readonly IMongoDatabase      Database;
        public readonly MongoClient         Client;
        public readonly MongoClientSettings Settings;
        public readonly string              AuthDataBase = "gemini_auth";

        public RepoBase(IConfigManager configManager)
        {
            ConnectionString = configManager.GetConnectionString("MongoClient");
            Settings         = MongoClientSettings.FromConnectionString(ConnectionString);
            Client           = new MongoClient(Settings);
            Database         = Client.GetDatabase(AuthDataBase);
        }

        public IMongoCollection<T> GetDBCollection<T>(string collectionName) =>
        Database.GetCollection<T>(collectionName);
    }
}
