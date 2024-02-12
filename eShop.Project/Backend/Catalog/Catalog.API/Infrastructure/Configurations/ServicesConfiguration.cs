namespace Catalog.API.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICatalogTypeRepository<CatalogTypeEntity>, CatalogTypeRepository>();
        builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();

        builder.Services.AddScoped<ICatalogBrandRepository<CatalogBrandEntity>, CatalogBrandRepository>();
        builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();

        builder.Services.AddScoped<ICatalogItemRepository<CatalogItemEntity>, CatalogItemRepository>();
        builder.Services.AddScoped<ICatalogItemService, CatalogItemService>();

        builder.Services.AddAutoMapper(
            typeof(EntityToModelMapperProfile),
            typeof(RequestToModelMapperProfile));

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();

        builder.Services.AddLogging(loggingBuilder =>
           loggingBuilder.AddSerilog(dispose: true));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

    }
}
