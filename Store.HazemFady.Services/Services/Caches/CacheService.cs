// Ignore Spelling: Redis

using StackExchange.Redis;
using Store.HazemFady.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Store.HazemFady.Services.Services.Caches
{
    public class CacheService : ICashService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }
        public async Task<string> GetCacheKeyAsync(string key)
        {
            var Res = await _database.StringGetAsync(key);
            if (Res.IsNullOrEmpty) return null;
            return Res.ToString();
        }

        public async Task SetInCacheAsync(string key, object Response, TimeSpan ExpireTime)
        {

            if (Response == null) return;
            var Options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await _database.StringSetAsync(key, JsonSerializer.Serialize(Response, Options), ExpireTime);

        }
    }
}
