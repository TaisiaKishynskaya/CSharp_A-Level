namespace Catalog.Data.Infrastructure;

public class CatalogDbInitializer
{
    public static void EnsureDatabaseCreated(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
