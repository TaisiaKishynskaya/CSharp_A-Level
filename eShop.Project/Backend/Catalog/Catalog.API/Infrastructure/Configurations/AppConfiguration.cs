namespace Catalog.API.Infrastructure.Configurations;

// Цей код відповідає за конфігурацію та налаштування ASP.NET Core додатку, зокрема середовища виконання, обробку винятків, аутентифікацію та авторизацію, налаштування БД та логування.

public static class AppConfiguration
{
    // це статичний метод, який виконує конфігурацію додатку. Він отримує об'єкт WebApplication, який представляє собою додаток ASP.NET Core.
    public static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment()) // Перевірка, чи додаток працює в середовищі розробки (IsDevelopment). Якщо так, то виконуються наступні дії:
        {
            app.UseSwagger(); // Використання Swagger для генерації
            app.UseSwaggerUI(); // та відображення документації API
        }

        app.UseMiddleware<ExceptionMiddleware>(); // Додавання middleware для обробки винятків. Це дозволяє обробляти винятки, що виникають під час обробки HTTP-запитів.
        // Використання middleware для аутентифікації та авторизації. Ці middleware дозволяють налаштувати механізми аутентифікації та авторизації для додатку.
        app.UseAuthentication();
        app.UseAuthorization();
        // Ця строка використовується для налаштування авторизації для контролерів.
        // app.MapControllers() - цей метод налаштовує маршрутизацію для контролерів в додатку. Він вказує, що всі HTTP-запити на контролери повинні бути оброблені за допомогою маршрутизації.
        // RequireAuthorization() - цей метод встановлює вимогу авторизації для всіх контролерів. Він вимагає, щоб кожен HTTP-запит на контролер був авторизований з використанням специфічного області авторизації "CatalogApiScope".
        app.MapControllers().RequireAuthorization("CatalogApiScope");

        CatalogDbInitializer.EnsureDatabaseCreated(app.Services); // Виклик методу EnsureDatabaseCreated класу CatalogDbInitializer для перевірки наявності та створення БД за необхідності.

        // Створення конфігурації логування (serilogConfig) на основі файлу конфігурації serilog.json. Файл конфігурації містить налаштування для бібліотеки логування Serilog.
        var serilogConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json")
            .Build();
        
        // Налаштування логера (Logger) на основі конфігурації serilogConfig. Це встановлює глобальний логер, який буде використовуватися для логування повідомлень у всьому додатку.
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(serilogConfig)
            .CreateLogger();
    }
}

// В цілому, цей код виконує налаштування додатку, встановлює обробники запитів та відповідей, налаштовує БД та логування для додатку ASP.NET Core.
