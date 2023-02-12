namespace Application.Token.DTOs
{
    public class LoginTokenDTO
    {
        public string   Authorization  { get; set; }
        public DateTime ExpiresAt      { get; set; }
    }
}
