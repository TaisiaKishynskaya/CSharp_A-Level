var builder = WebApplication.CreateBuilder(args);

AuthenticationConfiguration.ConfigureAuthentication(builder);

AuthorizationConfiguration.ConfigureAuthorization(builder);

ServicesConfiguration.ConfigureServices(builder);

SwaggerConfiguration.AddSwagger(builder.Services, builder.Configuration);

var app = builder.Build();

AppConfiguration.ConfigureApp(app);

app.Run();