using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "http://localhost:5002";
//        //options.Audience = "alevelwebsite.com";
//        options.Audience = "basket";
//        options.RequireHttpsMetadata = false;
//        options.SaveToken = true;
//        options.RefreshOnIssuerKeyNotFound = true;
//    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:5002";
        options.Audience = "basket";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudiences = new[] { "basket", "alevelwebsite.com" }
        };
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.RefreshOnIssuerKeyNotFound = true;
    });

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
})
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "eShop- Basket HTTP API",
        Version = "v1",
        Description = "The Basket Service HTTP API"
    });

    var authority = configuration["Authorization:Authority"];
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                TokenUrl = new Uri($"{authority}/connect/token"),
                Scopes = new Dictionary<string, string>()
                {
                    { "mvc", "website" },
                    { "basket.basketitem", "basket.basketitem" }
                }
            }
        }
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

//builder.AddConfiguration();

//builder.Services.AddAuthorization(configuration);


builder.AddConfiguration();

builder.Services.AddAuthorization(options =>
{
    configuration.GetSection("Authorization").Bind(options);

    options.AddPolicy("AnyScope", policy =>
      policy.RequireAssertion(context =>
          context.User.HasClaim(claim =>
              (claim.Type == "scope" && claim.Value == "basket.basketitem") ||
              (claim.Type == "scope" && claim.Value == "mvc"))));
});



builder.Services.AddTransient<IItemService, ItemService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

app.UseSwagger()
    .UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Basket.API V1");
        setup.OAuthClientId("basketswaggerui");
        setup.OAuthAppName("Basket Swagger UI");
    });

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}