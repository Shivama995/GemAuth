using Common.Application.Token;

namespace Application.Token.Services
{
    public interface IJwtTokenService
    {
        JwtTokenSettings GetJwtTokenSettings();
    }
}
