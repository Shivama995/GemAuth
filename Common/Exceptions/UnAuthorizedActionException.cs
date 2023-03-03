namespace Common.Exceptions
{
    public class UnAuthorizedActionException : Exception
    {
        public UnAuthorizedActionException() : base("You are unauthorized for this action!!") { }
        public UnAuthorizedActionException(string message) : base(message) { }
    }
}
