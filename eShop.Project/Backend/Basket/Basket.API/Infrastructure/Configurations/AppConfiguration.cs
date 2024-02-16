﻿namespace Basket.API.Infrastructure.Configurations;

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

        app.MapControllers().RequireAuthorization("BasketApiScope");

        var serilogConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(serilogConfig)
            .CreateLogger();
    }
}
