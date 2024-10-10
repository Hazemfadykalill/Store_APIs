// Ignore Spelling: redis
using StackExchange.Redis;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.HazemFady.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly  IDatabase database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            
            database=redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
           return await  database.KeyDeleteAsync(BasketId);
        }

        public async Task<UserBasket> GetBasketAsync(string BasketId)
        {
           var Basket=await database.StringGetAsync(BasketId);
            //Should be convert data from json string to UserBasket

            return Basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserBasket>(Basket);
        }

        public async  Task<UserBasket> UpdateBasketAsync(UserBasket userBasket)
        {
            var CreateOrUpdate=await database.StringSetAsync(userBasket.Id, JsonSerializer.Serialize(userBasket), TimeSpan.FromDays(30));
            if (CreateOrUpdate is false )
                return null;

            return await GetBasketAsync(userBasket.Id);
        }
    }
}
