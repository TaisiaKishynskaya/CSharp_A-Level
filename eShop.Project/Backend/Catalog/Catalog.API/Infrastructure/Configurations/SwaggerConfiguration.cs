namespace Catalog.API.Infrastructure.Configurations;

// Цей код відповідає за налаштування Swagger для ASP.NET Core додатку.

public static class SwaggerConfiguration
{
    // метод виконує налаштування Swagger. Він приймає параметри services, який є колекцією служб, що додаються, і configuration, який є об'єктом конфігурації додатку.
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        // додає підтримку Swagger до служб додатку. В методі AddSwaggerGen ми можемо налаштовувати Swagger за допомогою передачі конфігураційних параметрів через options.
        services.AddSwaggerGen(options =>
        {
            // Тут встановлюється визначення безпеки для Swagger.
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                // Ми створюємо схему безпеки OAuth2 з типом SecuritySchemeType.OAuth2, яка використовується для авторизації. 
                Type = SecuritySchemeType.OAuth2, // Для цього використовується клас OpenApiSecurityScheme з параметрами, такими як тип, потік авторизації та URL авторизації.
                Flows = new OpenApiOAuthFlows // вказуємо URL авторизації та області видимості.
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Swagger:AuthorizationUrl"]),
                        Scopes = new Dictionary<string, string>
                        {
                            { "CatalogAPI", "API - full access" },
                        },
                    },
                },
            });

            // Встановлюємо вимоги до безпеки для Swagger.
            options.AddSecurityRequirement(new OpenApiSecurityRequirement // Це робиться за допомогою класу OpenApiSecurityRequirement, що вказує на обов'язковість наявності певної схеми безпеки.
            {
                {
                    // вказуємо, що Swagger вимагає наявності токена авторизації для доступу до API.
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { "CatalogAPI" }
                }
            });
        });

        return services; // повертається колекція служб services, яка містить налаштування Swagger.
    }
}

// Отже, цей код встановлює конфігурацію Swagger для додатку, включаючи визначення безпеки та вимоги до неї. Swagger дозволяє генерувати документацію для API та тестувати його, що дуже корисно для розробки та документування API.
