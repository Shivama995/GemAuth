namespace Application.Authentication.Requests
{
    public class VerifyCredentialsDTO
    {
        public string EmailAddress { get; set; }
        public string Password     { get; set; }
    }
}
