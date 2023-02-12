using Application.Token.DTOs;

namespace Application.Authentication.Requests
{
    public class VerifyCredentialsDTO
    {
        public string        FirstName { get; set; }
        public LoginTokenDTO Token     { get; set; }
    }
}
