using Common.Application.Token;

namespace Application.Authentication.Requests
{
    public class VerifyCredentialsDTO
    {
        public string        FirstName { get; set; }
        public LoginTokenModel Token     { get; set; }
    }
}
