namespace Ordering.API.Infrastructure.Configurations;

public static class AuthorizationConfiguration
{
    public static void ConfigureAuthorization(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("OrderApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "OrderAPI");
            });
        });
    }
}
