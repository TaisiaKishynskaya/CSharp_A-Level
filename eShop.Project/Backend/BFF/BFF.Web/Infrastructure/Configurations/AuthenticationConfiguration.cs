namespace BFF.Web.Infrastructure.Configurations;

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
                  NameClaimType = ClaimTypes.NameIdentifier,
                  RoleClaimType = ClaimTypes.Role
              };
          });
    }
}
