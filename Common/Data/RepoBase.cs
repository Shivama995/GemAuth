using Common.Configuration;
using Common.Constants;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Common.Data
{
    public class RepoBase
    {
        public IMongoDatabase               Database { get; private set; }
        public readonly string              ConnectionString;
        public readonly MongoClient         Client;
        public readonly MongoClientSettings Settings;

        public RepoBase(IConfigManager configManager)
        {
            ConnectionString = configManager.GetConnectionString("MongoClient");
            Settings         = MongoClientSettings.FromConnectionString(ConnectionString);
            Client           = new MongoClient(Settings);
            Database         = Client.GetDatabase(DatabaseName.Config);
        }

        public IMongoCollection<T> GetDBCollection<T>(string collectionName) =>
            Database.GetCollection<T>(collectionName);

        public void LoadDatabase(string databaseName)
        {
            Database = Client.GetDatabase(databaseName);
        }
    }
}
