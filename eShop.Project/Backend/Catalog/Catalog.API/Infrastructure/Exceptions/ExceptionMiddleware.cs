namespace Catalog.API.Infrastructure.Exceptions;

// Цей код представляє клас, який є посередником ASP.NET Core і обробляє винятки, що виникають під час обробки HTTP-запитів.

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next; // приватне поле, яке зберігає наступний посередник у конвеєрі обробки HTTP-запитів.
    // Це словник, який містить відповідність між типом винятку і кодом статусу HTTP-відповіді. Коли виникає виняток, ми шукаємо відповідний код статусу HTTP у цьому словнику для винятку.
    private readonly Dictionary<Type, int> _exceptionStatusCodes = new()
    {
        { typeof(NotFoundException), StatusCodes.Status404NotFound },
        { typeof(FluentValidationException), StatusCodes.Status400BadRequest },
        { typeof(DbUpdateException), StatusCodes.Status409Conflict },
        { typeof(InvalidOperationException), StatusCodes.Status500InternalServerError },
        { typeof(ArgumentNullException), StatusCodes.Status400BadRequest },
        { typeof(NullReferenceException), StatusCodes.Status400BadRequest },
        { typeof(ArgumentOutOfRangeException), StatusCodes.Status400BadRequest },
        { typeof(ArgumentException), StatusCodes.Status400BadRequest },
        { typeof(ValidationAsyncException), StatusCodes.Status400BadRequest },
        { typeof(Exception), StatusCodes.Status500InternalServerError }
    };

    // Це конструктор класу, який приймає наступний посередник у конвеєрі.
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Цей метод викликається під час кожного HTTP-запиту і обробляє винятки, якщо вони виникають під час обробки запиту.
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // викликає наступний посередник у конвеєрі,
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex); // а коли виникає виняток, обробляє його за допомогою методу HandleExceptionAsync.
        }
    }

    // Цей метод обробляє виняток і встановлює відповідний код статусу HTTP для відповіді. 
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json"; // Встановлюємо заголовок ContentType для відповіді, щоб вказати, що ми надсилаємо JSON дані

        var exceptionType = ex.GetType(); // Отримуємо тип винятку, щоб дізнатися його клас

        // // Перевіряємо, чи містить словник _exceptionStatusCodes відповідний код статусу для типу винятку
        if (_exceptionStatusCodes.ContainsKey(exceptionType))
        {
            context.Response.StatusCode = _exceptionStatusCodes[exceptionType]; // Якщо так, встановлюємо відповідний код статусу HTTP
        }
        else
        {
            // В іншому випадку встановлюємо код статусу 500 (внутрішня помилка сервера)
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        // // Серіалізуємо об'єкт у формат JSON та записуємо його у відповідь сервера
        return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails() 
            // Створюємо об'єкт з кодом статусу та повідомленням про помилку
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message
        }));
    }
}
