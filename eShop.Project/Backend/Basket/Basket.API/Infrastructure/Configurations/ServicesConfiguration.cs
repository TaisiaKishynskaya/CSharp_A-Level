using Basket.API.Infrastructure.Validations;
using Helpers.Abstractions;

namespace Basket.API.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.Configure<ApiClientSettings>
            (builder.Configuration.GetSection("CatalogApiClientSettings"));

        builder.Services.AddScoped<IApiClientHelper, ApiClientHelper>();

        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<ICacheService, CacheService>();
        builder.Services.AddScoped<IBasketService, BasketService>();

        builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<ItemRequestValidator>();

        builder.Services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
}
