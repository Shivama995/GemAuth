namespace Common.Exceptions
{
    public class AppAlreadyExistsException : Exception
    {
        public AppAlreadyExistsException() : base("App with this name already exists!!") { }
        public AppAlreadyExistsException(string message) : base(message) { }
    }
}
