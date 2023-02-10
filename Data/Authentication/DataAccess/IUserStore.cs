namespace Data.Authentication.DataAccess
{
    public interface IUserStore
    {
        Task<string> GetUserAuthDetails(string emailAddress);
    }
}
