using Application.Token.DTOs;
using Common.Application.Token;
using Data.User.Models;

namespace Application.Token.Services
{
    public interface ILoginTokenService
    {
        Task<LoginTokenModel> CreateLoginToken(UserDetailsModel userDetails);
        Task<VerifyLoginTokenDTO> VerifyJwtToken(string token);
    }
}
