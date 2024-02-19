namespace Catalog.API.Infrastructure.Configurations;

// Даний код відповідає за налаштування аутентифікації для ASP.NET Core додатку. 

public static class AuthenticationConfiguration
{
    // Метод виконує налаштування аутентифікації. Він приймає параметр builder, який є об'єктом WebApplicationBuilder, використовуваним для налаштування додатку.
    public static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        // цей рядок отримує розділ конфігурації з назвою "Authentication" з конфігурації додатку. Цей розділ містить налаштування для аутентифікації, такі як авторитет (Authority) та інші параметри.
        var authSettings = builder.Configuration.GetSection("Authentication");

        builder.Services.AddAuthentication("Bearer") // вказує ASP.NET Core додатку використовувати аутентифікацію типу "Bearer" (для аутентифікації буде використовуватися механізм передачі токенів "Bearer").
            .AddJwtBearer("Bearer", options => // цей метод додає JwtBearer аутентифікацію. Він приймає назву схеми аутентифікації ("Bearer") та параметри налаштування для аутентифікації JwtBearer.
            {
                options.Authority = authSettings["Authority"]; // встановлює авторитет (Authority) для перевірки токенів. Авторитет вказує місце, де можна перевірити токени аутентифікації.
                // встановлює параметри перевірки токена. У даному випадку, ValidateAudience встановлюється в false, що означає, що аудиторія (audience) токена не буде перевірятися.
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                };
            });
    }
}

// Отже, цей код встановлює аутентифікацію для ASP.NET Core додатку з використанням JwtBearer аутентифікації та налаштовує параметри перевірки токенів.
