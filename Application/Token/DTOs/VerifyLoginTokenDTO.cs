using Data.User.Models;

namespace Application.Token.DTOs
{
    public class VerifyLoginTokenDTO
    {
        public UserAggregateModel UserData { get; set; }
    }
}
