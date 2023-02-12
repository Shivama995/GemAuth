namespace Common.Redis.Implementations
{
    public interface IRedis
    {
        Task PutValueInCache<T>(string key, T value, TimeSpan? expiry = null);
        Task<T> GetValueFromCache<T>(string key);
    }
}
