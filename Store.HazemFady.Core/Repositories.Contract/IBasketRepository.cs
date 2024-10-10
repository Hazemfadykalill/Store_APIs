using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Repositories.Contract
{
    public  interface IBasketRepository
    {

       Task<UserBasket> GetBasketAsync(string BasketId);
       Task<UserBasket> UpdateBasketAsync(UserBasket userBasket);
       Task<bool> DeleteBasketAsync(string BasketId);
    }
}
