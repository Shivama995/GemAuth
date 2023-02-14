using Application.Token.DTOs;
using Common.Application.Token;
using Common.Configuration;
using Common.Constants;
using Common.Cryptography;
using Common.Exceptions;
using Common.Extensions;
using Common.Redis.Implementations;
using Data.Authentication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Token.Services.Implementations
{
    public class LoginTokenService : JwtTokenService, ILoginTokenService
    {
        private readonly ICrypt _Crypt;
        private readonly IRedis _RedisStore;
        private readonly string _AuthTokenCacheKey = "AuthorizationToken:{0}";

        public LoginTokenService(IConfigManager configManager,
            ICrypt crypt,
            IRedis redisStore) : base(configManager)
        {
            _Crypt = crypt;
            _RedisStore = redisStore;
        }

        public async Task<LoginTokenModel> CreateLoginToken(UserDetailsModel userDetails)
        {
            var LoginTokenModel = await CreateLoginTokenModel(userDetails);
            var TokenExpiresIn = LoginTokenModel.ExpiresAt.ToUniversalTime() - DateTime.UtcNow;

            await UpdateTokenInCache(userDetails.Id, LoginTokenModel, TokenExpiresIn);
            return LoginTokenModel;
        }
        public async Task<VerifyLoginTokenDTO> VerifyJwtToken(string token)
        {
            var JwtToken = await ValidateToken(token);
            if (JwtToken.IsNull())
                throw new InvalidAuthorizationTokenException();
            var LoginTokenModel = await GetModelFromClaims(JwtToken);

            var TokenModel = await GetTokenFromCache(LoginTokenModel.Id);

            if (TokenModel.IsNull() || TokenModel.Authorization.IsEmpty() || !TokenModel.Authorization.Equals(token))
                throw new AuthorizationTokenExpiredException();

            //Populate when user details are set
            return new VerifyLoginTokenDTO { };
        }
        #region Private Methods
        private async Task<UserDetailsModel> GetModelFromClaims(JwtSecurityToken token)
        {
            var ClaimsData = token.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.AuthorizationClaimsData)?.Value;
            return await _Crypt.Decrypt<UserDetailsModel>(ClaimsData);
        }
        private async Task<LoginTokenModel> GetTokenFromCache(string userId)
        {
            return await _RedisStore.GetValueFromCache<LoginTokenModel>(string.Format(_AuthTokenCacheKey, userId));
        }
        private async Task UpdateTokenInCache(string userId, LoginTokenModel loginTokenModel, TimeSpan tokenExpiresIn)
        {
            await _RedisStore.PutValueInCache(string.Format(_AuthTokenCacheKey, userId), loginTokenModel, tokenExpiresIn);
        }
        private async Task<LoginTokenModel> CreateLoginTokenModel(UserDetailsModel userDetails)
        {
            var JwtTokenSettings = GetJwtTokenSettings();
            var JwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var TokenDescriptor = await GetSecurityTokenDescriptor(userDetails, JwtTokenSettings);
            SecurityToken AuthorizationToken = JwtSecurityTokenHandler.CreateToken(TokenDescriptor);

            return new LoginTokenModel
            {
                Authorization = JwtSecurityTokenHandler.WriteToken(AuthorizationToken),
                ExpiresAt = Convert.ToDateTime(TokenDescriptor.Expires).ToLocalTime()
            };
        }
        private async Task<SecurityTokenDescriptor> GetSecurityTokenDescriptor(UserDetailsModel userDetails, JwtTokenSettings jwtTokenSettings)
        {
            var ClaimsIdentity = await GetClaimsIdentity(userDetails);
            double.TryParse(jwtTokenSettings.ExpiryInMinutes, out double expiryInMinutes);

            return new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(jwtTokenSettings.SecretKey),
                SecurityAlgorithms.HmacSha256Signature,
                SecurityAlgorithms.Sha256Digest),


                Subject = ClaimsIdentity,
                Audience = jwtTokenSettings.Audience,
                Issuer = jwtTokenSettings.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes <= 0 ? 600 : expiryInMinutes)
            };
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(UserDetailsModel userDetails)
        {
            return new ClaimsIdentity(new List<Claim>()
            {
                new Claim(CustomClaimTypes.AuthorizationClaimsData, await _Crypt.Encrypt(new TokenPayload
                {
                    Id           = userDetails.Id,
                    EmailAddress = userDetails.EmailAddress
                }))
            });
        }

        private async Task<JwtSecurityToken> ValidateToken(string token)
        {
            var JwtTokenSettings = GetJwtTokenSettings();
            var JwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSymmetricSecurityKey(JwtTokenSettings.SecretKey),
                ValidAudiences = JwtTokenSettings.Audience?.Split(","),
                ValidIssuers = JwtTokenSettings.Issuer?.Split(",")
            };

            JwtSecurityTokenHandler.ValidateToken(token, TokenValidationParameters, out SecurityToken ValidatedToken);
            if (ValidatedToken.IsNull())
                throw new InvalidAuthorizationTokenException();

            return ValidatedToken as JwtSecurityToken;
        }
        #endregion
    }
}
