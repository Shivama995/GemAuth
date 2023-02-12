namespace Common.Cryptography
{
    public interface ICrypt
    {
        Task<string> Encrypt<T>(T model);
        Task<T> Decrypt<T>(string PlainText);
    }
}
