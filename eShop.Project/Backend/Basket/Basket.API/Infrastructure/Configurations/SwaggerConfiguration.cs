namespace Basket.API.Infrastructure.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Swagger:AuthorizationUrl"]),
                        Scopes = new Dictionary<string, string>
                        {
                            { "BasketAPI", "API - full access" },
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
                    new[] { "BasketAPI" }
                }
            });
        });

        return services;
    }
}
