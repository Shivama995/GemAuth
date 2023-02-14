namespace Common.Application.Token
{
    public class LoginTokenModel
    {
        public string   Authorization  { get; set; }
        public DateTime ExpiresAt      { get; set; }
    }
}
