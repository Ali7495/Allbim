using Common.Extensions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Services.Redis
{
    public class RedisService : IRedisService
    {
        #region Fileds

        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _db;
        #endregion

        #region Ctor

        public RedisService(/*IConnectionMultiplexer connectionMultiplexer*/ IConfiguration configuration)
        {
            //_connectionMultiplexer = connectionMultiplexer;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("redisConnectionString"));

            _db = _connectionMultiplexer.GetDatabase();
        }
        #endregion

        #region Get
        public async Task<string> GetValueAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }
        public async Task<T> GetValueAsync<T>(string key)
        {
            var value = (await _db.StringGetAsync(key));
            if (value.IsNullOrEmpty)
                return default(T);

            return value.ToString().ToObject<T>();
        }
        #endregion

        #region Set

        public async Task SetValueAsync(string key, string value, TimeSpan? expirationTime = null)
        {
            await _db.StringSetAsync(key, value, expirationTime);
        }
        public async Task SetValueAsync(string key, object obj, TimeSpan? expirationTime = null)
        {
            await _db.StringSetAsync(key, obj.ToJson(), expirationTime);
        }
        #endregion
    }
}
