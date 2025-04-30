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
        try
        {
            var apiKey = context.Request.Headers["ApiKey"].ToString();

            if (string.IsNullOrEmpty(apiKey))
                throw new TooManyRequestException("API Key is missing");

            var cacheKey = $"ratelimit_{apiKey}";
            var currentCount = cache.Get<int?>(cacheKey) ?? 0;

            if (currentCount >= Limit)
                throw new TooManyRequestException("Too many requests. Please try again later.");

            cache.Set(cacheKey, currentCount + 1, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Duration)
            });

            await next(context);
        }
        catch (TooManyRequestException ex)
        {
            await SetContext(context, ex.Message, StatusCodes.Status429TooManyRequests);
        }
        catch (Exception ex)
        {
            await SetContext(context, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task SetContext(HttpContext context, string message, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = message,
            statusCode = statusCode
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

