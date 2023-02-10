using Microsoft.Extensions.Configuration;

namespace Common.Configuration
{
    public interface IConfigManager
    {
        IConfiguration GetConfiguration();
    }
}
