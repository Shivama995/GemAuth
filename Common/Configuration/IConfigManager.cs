using Microsoft.Extensions.Configuration;

namespace Common.Configuration
{
    public interface IConfigManager
    {
        IConfiguration GetConfiguration();
        string         GetConnectionString(string key);
        T              GetSection<T>(string key);
        string         Get(string key);
    }
}
