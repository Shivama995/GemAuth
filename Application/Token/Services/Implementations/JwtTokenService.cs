using Common.Application.Token;
using Common.Configuration;
using Common.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Application.Token.Services.Implementations
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfigManager        _ConfigManager;
        public JwtTokenService(IConfigManager configManager) 
        {
            _ConfigManager   = configManager;
        }

        public JwtTokenSettings GetJwtTokenSettings() =>
            _ConfigManager.GetSection<JwtTokenSettings>("JwtTokenSettings");

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey) =>
                new SymmetricSecurityKey(secretKey.GetBytes());
    }
}
