using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Logging;
using Store.HazemFady.Core.Services.Contract;
using System.Text;

namespace Store.HazemFady.APIs.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int expireTime;

        public CachedAttribute(int ExpireTime)
        {
            expireTime = ExpireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var CacheRes=await CacheService.GetCacheKeyAsync(cacheKey);
            if (!string.IsNullOrEmpty(CacheRes))
            {
                var ContentResult = new ContentResult()
                {
                    Content = CacheRes,
                    StatusCode = 200,
                    ContentType = "application/json"
                };
                context.Result = ContentResult;
                return;
            }

           var ExcutedContext=await  next();

            if(ExcutedContext.Result is OkObjectResult response)
            {
                await CacheService.SetInCacheAsync(cacheKey,response.Value,TimeSpan.FromSeconds (expireTime));
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                cacheKey.Append($"|{key}-- {value}");
            }
            return cacheKey.ToString();
        }
    }
}
