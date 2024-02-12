public class CacheService : ICacheService
{
    private readonly IDatabase _database;

    public CacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<string> Get(string key) => await _database.StringGetAsync(key);

    public async Task<bool> Set(string key, string value) => await _database.StringSetAsync(key, value);

    public async Task<bool> Delete(string key) => await _database.KeyDeleteAsync(key);

}