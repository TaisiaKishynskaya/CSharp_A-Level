using Serilog;

namespace Identity.API;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables().Build();

        builder.Services.AddRazorPages();

        builder.Services.AddIdentityServer(options =>
        {
            options.EmitStaticAudienceClaim = true;
        })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });


        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseIdentityServer();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

        return app;
    }
}