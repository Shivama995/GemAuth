using Common.Enums;

namespace Application.User.DTOs
{
    public class GetUserDTO
    {
        public string FirstName    { get; set; }
        public string LastName     { get; set; }
        public string EmailAddress { get; set; }
        public UserRole Role       { get; set; }
    }
}
