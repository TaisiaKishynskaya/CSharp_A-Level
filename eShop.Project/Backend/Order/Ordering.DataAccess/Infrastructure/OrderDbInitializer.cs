using Microsoft.Extensions.DependencyInjection;

namespace Ordering.DataAccess.Infrastructure;

public class OrderDbInitializer
{
    //  Это статический метод, который выполняет инициализацию БД. Он принимает IServiceProvider, который предоставляет доступ к сервисам в приложении.
    public static void EnsureDatabaseCreated(IServiceProvider serviceProvider)
    {
        // Этот блок создает область (scope) для работы с сервисами. Область (scope) обеспечивает правильную очистку ресурсов после завершения использования сервисов.
        using (var scope = serviceProvider.CreateScope())
        {
            // Эта строка получает экземпляр CatalogDbContext из контейнера зависимостей (DI container) с помощью IServiceProvider.
            var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
            // тот вызов метода гарантирует, что БД создана.
            // Если БД уже существует, этот метод ничего не делает, иначе он создает новую БД на основе модели данных, предоставленной CatalogDbContext.
            context.Database.EnsureCreated();
        }
    }
}