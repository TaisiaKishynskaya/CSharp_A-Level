namespace WebApp.Infrastructure.Configurations;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var authSettings = builder.Configuration.GetSection("Authentication");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = authSettings["Authority"];
                options.ClientId = authSettings["ClientId"];
                options.ClientSecret = authSettings["ClientSecret"];
                options.ResponseType = authSettings["ResponseType"];
                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;
            });
    }
}
