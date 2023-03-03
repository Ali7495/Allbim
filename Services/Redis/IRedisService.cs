using System;
using System.Threading.Tasks;

namespace Services.Redis
{
    public interface IRedisService
    {
        public Task<string> GetValueAsync(string key);
        public Task<T> GetValueAsync<T>(string key);
        public Task SetValueAsync(string key, string value, TimeSpan? expirationTime = null);
        public Task SetValueAsync(string key, object obj, TimeSpan? expirationTime = null);
    }
}
