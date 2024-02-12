namespace Basket.API.Infrastructure.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(x =>
        {
            var configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configurationOptions);
        });
    }
}
