using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Infrastructure.RateLimit.Interfaces;

namespace Infrastructure.RateLimit;

public class RateLimitFilter : IAsyncActionFilter, IRateLimitFilter
{
    private readonly IDatabase _redis;

    public RateLimitFilter(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
        var endpoint = context.HttpContext.Request.Path;

        var key = $"{ip}:{endpoint}";

        var currentRequestCount = await _redis.StringIncrementAsync(key, 1);

        if (currentRequestCount == 1)
        {
            await _redis.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
        }

        if (currentRequestCount > 10)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status429TooManyRequests);
            return;
        }

        await next();
    }
}