using Common.Configuration;
using Common.Extensions;
using StackExchange.Redis;

namespace Common.Redis.Implementations
{
    public class Redis : IRedis
    {
        #region Private Members
        private readonly IDatabase _RedisConnection;
        private readonly string    _CacheSignature = "[Gemini]:";
        #endregion

        #region Constructor
        public Redis(IConfigManager configManager)
        {
            _RedisConnection = ConnectionMultiplexer.Connect(configManager.GetConnectionString("RedisClient")).GetDatabase();
        }
        #endregion

        #region Public Methods
        public async Task PutValueInCache<T>(string key, T value, TimeSpan? expiry = null)
        {
            key = string.Concat(_CacheSignature, key);
            string Value = value.Serialize();
            
            await _RedisConnection.StringSetAsync(key, Value, expiry: expiry, When.Always, CommandFlags.DemandMaster);
        }

        public async Task<T> GetValueFromCache<T>(string key)
        {
            key = string.Concat(_CacheSignature, key);
            string Value = await _RedisConnection.StringGetAsync(key, CommandFlags.DemandMaster);
            T value = default;
            if (Value.IsNotNull())
            {
                value = Value.Deserialize<T>();
            }
            return value;
        }
        #endregion 
    }
}
