namespace Catalog.API.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    // виконує налаштування служб. Він приймає параметр builder, що є об'єктом WebApplicationBuilder, використовуваним для налаштування додатку.
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // додає службу типу ICatalogTypeRepository з реалізацією CatalogTypeRepository в контейнер служб додатку з областю дії "Scoped".
        // Це дозволяє зареєструвати службу, яка буде створюватися один раз для кожного обробленого запиту HTTP.
        // Аналогічно далее рядки реєструють різні служби для каталогу типів, брендів та елементів.
        builder.Services.AddScoped<ICatalogTypeRepository<CatalogTypeEntity>, CatalogTypeRepository>();
        builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();

        builder.Services.AddScoped<ICatalogBrandRepository<CatalogBrandEntity>, CatalogBrandRepository>();
        builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();

        builder.Services.AddScoped<ICatalogItemRepository<CatalogItemEntity>, CatalogItemRepository>();
        builder.Services.AddScoped<ICatalogItemService, CatalogItemService>();

        // додає AutoMapper для відображення між моделями. Він вказує, які профілі мапування використовувати.
        builder.Services.AddAutoMapper(
            typeof(EntityToModelMapperProfile),
            typeof(RequestToModelMapperProfile));

        // ці рядки додають підтримку валідації FluentValidation для валідації моделей на сервері та клієнті.
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();

        // додає підтримку логування за допомогою Serilog.
        builder.Services.AddLogging(loggingBuilder =>
           loggingBuilder.AddSerilog(dispose: true));

        builder.Services.AddControllers(); // додає підтримку контролерів до додатку.
        builder.Services.AddEndpointsApiExplorer(); // додає підтримку API Explorer для документації API.
    }
}

// Отже, цей код встановлює та налаштовує різні служби, які використовуються в ASP.NET Core додатку для роботи з каталогом товарів.
