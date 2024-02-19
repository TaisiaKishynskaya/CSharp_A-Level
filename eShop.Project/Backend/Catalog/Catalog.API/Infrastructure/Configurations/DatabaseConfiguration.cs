namespace Catalog.API.Infrastructure.Configurations;

// Цей код встановлює з'єднання з БД і налаштовує контекст БД для додатку.

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("CatalogDb"); // цей рядок отримує рядок підключення до БД з файлу конфігурації додатку за допомогою ключа "CatalogDb".
        // цей рядок додає контекст БД до служб додатку. Метод AddDbContext вказує, що використовуватиметься контекст БД типу CatalogDbContext. Метод UseNpgsql встановлює тип БД (PostgreSQL) та рядок підключення. Опція b.MigrationsAssembly вказує, де будуть знаходитися міграції БД.
        builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Catalog.API")));
    }
}
