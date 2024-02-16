namespace Ordering.API.Infrastructure.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<IApiClientHelper, ApiClientHelper>();

        builder.Services.Configure<CatalogApiClientSettings>
            (builder.Configuration.GetSection("CatalogApiClientSettings"));

        builder.Services.Configure<BasketApiClientSettings>
          (builder.Configuration.GetSection("BasketApiClientSettings"));

        builder.Services.AddScoped<IUserRepository<UserEntity>, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IOrderRepository<OrderEntity>, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddScoped<ITransactionService, TransactionService>();

        builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<ItemRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<OrderItemUpdateRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<OrderRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<OrderUpdateRequestValidator>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
}
