namespace Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User with these credentials not found!") { }
        public UserNotFoundException(string message) : base(message) { }
    }
}
