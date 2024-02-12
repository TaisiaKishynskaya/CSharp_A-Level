var builder = WebApplication.CreateBuilder(args);

AuthenticationConfiguration.ConfigureAuthentication(builder);

AuthorizationConfiguration.ConfigureAuthorization(builder);

SwaggerConfiguration.AddSwagger(builder.Services, builder.Configuration);

DatabaseConfiguration.ConfigureRedis(builder.Services, builder.Configuration);

ServicesConfiguration.ConfigureServices(builder);

var app = builder.Build();

AppConfiguration.ConfigureApp(app);

app.Run();
