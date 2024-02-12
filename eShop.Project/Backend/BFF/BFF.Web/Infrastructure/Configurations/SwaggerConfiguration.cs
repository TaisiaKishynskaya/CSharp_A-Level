namespace BFF.Web.Infrastructure.Configurations;
public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerSettings = configuration.GetSection("Swagger");

        services.AddSwaggerGen(options =>
        {
            // Scheme Definition 
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(swaggerSettings["AuthorizationUrl"]),
                        TokenUrl = new Uri(swaggerSettings["TokenUrl"]),
                        Scopes = new Dictionary<string, string>
                        {
                            { "WebBffAPI", "API - full access" }
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

        return services;
    }
}