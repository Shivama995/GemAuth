namespace Common.Exceptions
{
    public class AuthorizationTokenExpiredException : Exception
    {
        public AuthorizationTokenExpiredException() : base("Authorization Token Expired!!") { }
        public AuthorizationTokenExpiredException(string message) : base(message) { }
    }
}
