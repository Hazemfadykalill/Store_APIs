using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Services.Contract
{
    public  interface ICashService
    {

        //func To Get Data From Database To Set In Cache
        Task SetInCacheAsync(string key, object Response,TimeSpan ExpireTime);


        //func To Get Data From Cache
        Task<string> GetCacheKeyAsync(string key);
    }
}
