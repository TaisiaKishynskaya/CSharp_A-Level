var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CatalogApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "CatalogAPI");
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow // Changed from AuthorizationCode to Implicit for Swagger
            {
                AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                Scopes = new Dictionary<string, string>
                {
                    {  "CatalogAPI", "API - full access" },
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

var connectionString = builder.Configuration.GetConnectionString("CatalogDb");
builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Catalog.API")));

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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddSerilog(dispose: true));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthUsePkce();
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

app.Run();
