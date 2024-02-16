namespace Ordering.API.Infrastructure.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("OrderDb");
        builder.Services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Order.API")));
    }
}
