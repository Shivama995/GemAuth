using Application.Token.DTOs;
using Data.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Token.Services
{
    public interface ILoginTokenService
    {
        Task<LoginTokenDTO> CreateLoginToken(UserDetailsModel userDetails);
        Task VerifyJwtToken(string token);
    }
}
