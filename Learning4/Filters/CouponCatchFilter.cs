using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Learning4.Filters
{
    public class CouponCatchFilter : IAsyncResourceFilter
    {
        private readonly IMemoryCache _cache;

        public CouponCatchFilter(IMemoryCache cache) {
        _cache = cache;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var cacheKey = context.HttpContext.Request.Path.ToString();
            if (_cache.TryGetValue(cacheKey, out IActionResult cahedResult))
            {
                context.Result = cahedResult;
                return;
            }
            var executedContext = await next();
            if(executedContext.Result is ViewResult okResult)
            {
                _cache.Set(cacheKey, okResult, TimeSpan.FromMinutes(1));
            }
        }
    }
}
