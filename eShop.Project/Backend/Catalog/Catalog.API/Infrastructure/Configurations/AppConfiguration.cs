namespace Catalog.API.Infrastructure.Configurations;

public static class AppConfiguration
{
    public static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        //app.MapControllers().RequireAuthorization("CatalogApiScope");

        CatalogDbInitializer.EnsureDatabaseCreated(app.Services);

        var serilogConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(serilogConfig)
            .CreateLogger();
    }
}
