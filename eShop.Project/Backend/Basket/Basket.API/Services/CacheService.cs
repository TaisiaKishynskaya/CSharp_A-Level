using Basket.Core.Abstractions;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Claims;

namespace Basket.API.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _database;

    public CacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<string> Get(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task<bool> Set(string key, string value)
    {
        return await _database.StringSetAsync(key, value);
    }

    public async Task<bool> Delete(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }
}
