namespace WebApp.Infrastructure.Configurations;

public static class AppConfiguration
{
    public static void ConfigureApp(WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error/Error");
        }
        app.UseStaticFiles();

        app.UseSession();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Catalog}/{action=Catalog}/{id?}");


        var serilogConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(serilogConfig)
            .CreateLogger();
    }
}