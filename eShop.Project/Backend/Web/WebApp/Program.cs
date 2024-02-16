var builder = WebApplication.CreateBuilder(args);

AuthenticationConfiguration.ConfigureAuthentication(builder);

ServicesConfiguration.ConfigureServices(builder);

var app = builder.Build();

AppConfiguration.ConfigureApp(app);

app.Run();