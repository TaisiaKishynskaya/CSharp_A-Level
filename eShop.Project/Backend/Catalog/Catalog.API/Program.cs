var builder = WebApplication.CreateBuilder(args);

// Configure Authentication
ConfigureAuthentication(builder);

// Configure Authorization
ConfigureAuthorization(builder);

// Configure Swagger
ConfigureSwagger(builder);

// Configure Database
ConfigureDatabase(builder);

// Configure Services
ConfigureServices(builder);

// Configure Controllers
ConfigureControllers(builder);

// Configure Logging
ConfigureLogging(builder);

var app = builder.Build();

// Configure Swagger UI and Middleware
ConfigureApp(app);

app.Run();

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://localhost:5001";
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
            };
        });
}

void ConfigureAuthorization(WebApplicationBuilder builder)
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("CatalogApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "CatalogAPI");
        });
    });
}

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                    Scopes = new Dictionary<string, string>
                    {
                        { "CatalogAPI", "API - full access" },
                    },
                },
            },
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                },
                new[] { "CatalogAPI" }
            }
        });
    });
}

void ConfigureDatabase(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("CatalogDb");
    builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Catalog.API")));
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ICatalogTypeRepository<CatalogTypeEntity>, CatalogTypeRepository>();
    builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();

    builder.Services.AddScoped<ICatalogBrandRepository<CatalogBrandEntity>, CatalogBrandRepository>();
    builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();

    builder.Services.AddScoped<ICatalogItemRepository<CatalogItemEntity>, CatalogItemRepository>();
    builder.Services.AddScoped<ICatalogItemService, CatalogItemService>();

    builder.Services.AddAutoMapper(
        typeof(EntityToModelMapperProfile),
        typeof(ModelToResponseMapperProfile),
        typeof(RequestToModelMapperProfile));

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();
    builder.Services.AddValidatorsFromAssemblyContaining<CatalogTypeRequestValidator>();
}

void ConfigureControllers(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
}

void ConfigureLogging(WebApplicationBuilder builder)
{
    builder.Services.AddLogging(loggingBuilder =>
        loggingBuilder.AddSerilog(dispose: true));
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
        });
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseAuthorization();
    app.MapControllers().RequireAuthorization("CatalogApiScope");

    CatalogDbInitializer.EnsureDatabaseCreated(app.Services);

    var serilogConfig = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("serilog.json")
        .Build();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(serilogConfig)
        .CreateLogger();
}