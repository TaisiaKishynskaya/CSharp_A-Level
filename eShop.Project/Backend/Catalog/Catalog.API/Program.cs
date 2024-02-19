// Этот код используется для настройки и запуска веб-приложения с использованием ASP.NET Core. 

// Создается объект builder, который представляет собой фабрику для создания веб-приложения.
var builder = WebApplication.CreateBuilder(args); // Создается новый экземпляр WebApplicationBuilder на основе переданных аргументов командной строки args.

// Вызывается метод ConfigureAuthentication из класса AuthenticationConfiguration, который настраивает аутентификацию в приложении.
AuthenticationConfiguration.ConfigureAuthentication(builder); // В метод передается объект builder, который содержит конфигурацию приложения.

// Вызывается метод ConfigureAuthorization из класса AuthorizationConfiguration, который настраивает авторизацию в приложении.
AuthorizationConfiguration.ConfigureAuthorization(builder); // В метод передается объект builder, который содержит конфигурацию приложения.

// Вызывается метод AddSwagger из класса SwaggerConfiguration, который добавляет поддержку Swagger в приложение.
SwaggerConfiguration.AddSwagger(builder.Services, builder.Configuration); // В метод передаются сервисы и конфигурация приложения из объекта builder.

// Вызывается метод ConfigureDatabase из класса DatabaseConfiguration, который настраивает подключение к бД в приложении.
DatabaseConfiguration.ConfigureDatabase(builder); // В метод передается объект builder, который содержит конфигурацию приложения.

// Вызывается метод ConfigureServices из класса ServicesConfiguration, который настраивает службы (services) приложения.
ServicesConfiguration.ConfigureServices(builder); // В метод передается объект builder, который содержит конфигурацию приложения.

// Вызывается метод Build() для создания объекта WebApplication. Тут все предыдущие настройки и конфигурации применяются к веб-приложению.
var app = builder.Build();

// Вызывается метод ConfigureApp из класса AppConfiguration, что дополнительно настраивает приложение (например, подключает Swagger UI в режиме разработки).
AppConfiguration.ConfigureApp(app); // В метод передается объект app, представляющий созданное веб-приложение.

app.Run();