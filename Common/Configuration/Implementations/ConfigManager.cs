using Microsoft.Extensions.Configuration;

namespace Common.Configuration.Implementations
{
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration _Configuration;

        public ConfigManager(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public IConfiguration GetConfiguration()
        {
            return _Configuration;
        }
        public string GetConnectionString(string key)
        {
            try
            {
                return _Configuration.GetConnectionString(key);
            }
            catch
            {
            }

            return null;
        }
        public T GetSection<T>(string key)
        {
            try
            {
                return _Configuration.GetSection(key).Get<T>();
            }
            catch
            {
            }

            return default(T);
        }
        public string Get(string key)
        {
            try
            {
                return _Configuration[key];
            }
            catch
            {
            }

            return null;
        }
    }
}
