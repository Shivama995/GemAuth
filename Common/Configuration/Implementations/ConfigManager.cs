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
    }
}
