using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace Infrastructure.RateLimit;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDatabase _redis;

    public RateLimitMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
    {
        _next = next;
        _redis = redis.GetDatabase();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress.ToString();
        var endpoint = context.Request.Path;

        var key = $"{ip}:{endpoint}";

        var currentRequestCount = await _redis.StringIncrementAsync(key, 1);

        if (currentRequestCount == 1)
        {
            await _redis.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
        }

        if (currentRequestCount > 10)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            return;
        }

        await _next(context);
    }
}