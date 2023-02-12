namespace Common.Application.Token
{
    public class JwtTokenSettings
    {
        public string Audience        { get; set; }
        public string Issuer          { get; set; }
        public string ExpiryInMinutes { get; set; }
        public string SecretKey       { get; set; }
    }
}
