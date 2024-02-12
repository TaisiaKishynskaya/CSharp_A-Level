namespace Catalog.API.Infrastructure.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("CatalogDb");
        builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Catalog.API")));
    }
}
