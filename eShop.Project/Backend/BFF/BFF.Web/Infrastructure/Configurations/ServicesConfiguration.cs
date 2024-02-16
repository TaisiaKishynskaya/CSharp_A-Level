using Helpers.Abstractions;

namespace BFF.Web.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.Configure<CatalogApiClientSettings>
            (builder.Configuration.GetSection("CatalogApiClientSettings"));

        builder.Services.Configure<BasketApiClientSettings>
            (builder.Configuration.GetSection("BasketApiClientSettings"));

        builder.Services.Configure<OrderApiClientSettings>
            (builder.Configuration.GetSection("OrderApiClientSettings"));

        builder.Services.AddScoped<IApiClientHelper, ApiClientHelper>();

        builder.Services.AddScoped<ICatalogBffService, CatalogBffService>();
        builder.Services.AddScoped<IBasketBffService, BasketBffService>();
        builder.Services.AddScoped<IOrderBffService, OrderBffService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<ItemRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<OrderRequestValidator>();

        builder.Services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true)); 

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
}
