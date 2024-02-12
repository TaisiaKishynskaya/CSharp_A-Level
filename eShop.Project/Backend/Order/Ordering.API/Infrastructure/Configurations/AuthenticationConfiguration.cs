namespace Ordering.API.Infrastructure.Configurations;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var authSettings = builder.Configuration.GetSection("Authentication");

        builder.Services.AddAuthentication("Bearer")
          .AddJwtBearer("Bearer", options =>
          {
              options.Authority = authSettings["Authority"];
              options.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidateAudience = false,
              };
          });
    }
}
