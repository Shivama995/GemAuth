namespace Common.Exceptions
{
    public class AuthorizationTokenMissingException : Exception
    {
        public AuthorizationTokenMissingException() : base("Authorization Token Missing") { }
        public AuthorizationTokenMissingException(string message) : base(message) { }
    }
}
