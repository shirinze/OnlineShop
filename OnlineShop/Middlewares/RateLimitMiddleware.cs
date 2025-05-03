using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Exceptions;
using System.Collections.Generic;
using System.Text.Json;

namespace OnlineShop.Middlewares;

public class RateLimitMiddleware(RequestDelegate next, IMemoryCache cache)
{
    private const int Limit = 10;
    private const int Duration = 60;

    public async Task Invoke(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress!.ToString();
        var cacheKey = $"ratelimit_{ip}";
        var currentCount = cache.Get<int?>(cacheKey) ?? 0;

        if (currentCount >= Limit)
            throw new TooManyRequestException("Too many requests. Please try again later.");

        cache.Set(cacheKey, currentCount + 1, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Duration)
        });
        await next(context);
    }

}