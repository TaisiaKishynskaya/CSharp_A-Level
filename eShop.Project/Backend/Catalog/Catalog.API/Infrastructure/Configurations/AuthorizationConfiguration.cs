namespace Catalog.API.Infrastructure.Configurations;

// Цей код відповідає за налаштування авторизації для ASP.NET Core додатку.

public static class AuthorizationConfiguration
{
    // Метод виконує налаштування авторизації. Приймає параметр builder, який є об'єктом WebApplicationBuilder, використовуваним для налаштування додатку.
    public static void ConfigureAuthorization(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options => // додає службу авторизації до сервісів додатку. Метод AddAuthorization дозволяє налаштувати політики авторизації.
        {
            // додає нову політику авторизації з назвою "CatalogApiScope". Політика визначає умови, які повинні бути виконані для отримання доступу до ресурсів.
            options.AddPolicy("CatalogApiScope", policy =>
            {
                policy.RequireAuthenticatedUser(); // метод встановлює умову, за якою користувач повинен бути аутентифікованим для отримання доступу до ресурсу.
                // метод встановлює умову, за якою користувач повинен мати певний клейм (claim) з певним значенням.
                policy.RequireClaim("scope", "CatalogAPI"); //  У даному випадку, користувач повинен мати клейм з назвою "scope" і значенням "CatalogAPI".
            });
        });
    }
}

// Отже, цей код встановлює політику авторизації для додатку, яка вимагає аутентифікації користувача та наявності певного клейма для отримання доступу до ресурсів.
