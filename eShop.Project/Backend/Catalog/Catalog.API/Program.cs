var builder = WebApplication.CreateBuilder(args);

AuthenticationConfiguration.ConfigureAuthentication(builder);

AuthorizationConfiguration.ConfigureAuthorization(builder);

SwaggerConfiguration.AddSwagger(builder.Services, builder.Configuration);

DatabaseConfiguration.ConfigureDatabase(builder);

ServicesConfiguration.ConfigureServices(builder);

var app = builder.Build();

AppConfiguration.ConfigureApp(app);

app.Run();