using BFF.Web.Services;
using BFF.Web.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();

var connectionString = builder.Configuration.GetConnectionString("CatalogDb");
builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<ICatalogService, CatalogService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Configure Bearer as the default Authentication Scheme
// Add Jwt Bearer authentication services to the service collection to allow for dependency injection
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options => {
        options.Authority = "https://localhost:5001";
        // Our API app will call to the IdentityServer to get the authority
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false, 
            //ValidAudiences = new List<string> { "https://localhost:5001/resources" }
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "WebBffAPI");
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    // Scheme Definition 
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                            {
                                {"WebBffAPI", "WebBff API - full access"}
                            }
            },
        }
    });
    // Apply Scheme globally
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { "WebBffAPI" }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthUsePkce();
    });
}

app.UseAuthorization();

app.MapControllers().RequireAuthorization("ApiScope");

app.Run();