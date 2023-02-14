namespace Application.User.CommandHandlers
{
    public class AddUserCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string OrgCode { get; set; }
    }

    public class AddUserCommandHandler
    {
    }
}
