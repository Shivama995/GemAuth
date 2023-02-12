using Application.Token.DTOs;
using Data.Authentication.Models;

namespace Application.Token.Services
{
    public interface ILoginTokenService
    {
        Task<LoginTokenDTO> CreateLoginToken(UserDetailsModel userDetails);
    }
}
