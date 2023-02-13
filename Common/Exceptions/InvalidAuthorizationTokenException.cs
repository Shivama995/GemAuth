namespace Common.Exceptions
{
    public class InvalidAuthorizationTokenException : Exception
    {
        public InvalidAuthorizationTokenException() : base("Invalid Token!!") { }
        public InvalidAuthorizationTokenException(string message) : base(message) { }
    }
}
