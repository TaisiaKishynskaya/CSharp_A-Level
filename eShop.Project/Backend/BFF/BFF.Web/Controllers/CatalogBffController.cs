namespace BFF.Web.Controllers;

// Этот код представляет собой контроллер ASP.NET Core Web API, который обрабатывает HTTP-запросы для работы с каталогом продуктов через интерфейс BFF (Backend For Frontend).

[ApiController] // Этот атрибут указывает, что класс контроллера является контроллером API.
[Route("/bff/catalog")] // Этот атрибут устанавливает базовый маршрут для всех методов контроллера.
[Authorize(Policy = "ApiScope")] // Этот атрибут указывает, что для доступа к методам контроллера требуется аутентификация с использованием определенной политики безопасности, в данном случае "ApiScope".
public class CatalogBffController : ControllerBase
{
    private readonly ICatalogBffService _catalogBffService; // Это интерфейс сервиса, который определяет методы для работы с каталогом продуктов через BFF.

    public CatalogBffController(ICatalogBffService catalogBffService)
    {
        _catalogBffService = catalogBffService;
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {

        var brands = await _catalogBffService.GetBrands(page, size);
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes(int page = 1, int size = 3)
    {
        var types = await _catalogBffService.GetTypes(page, size);
        return Ok(types);
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems(int page = 1, int size = 10)
    {
        var items = await _catalogBffService.GetItems(page, size);
        return Ok(items);
    }

    [HttpGet("items/{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _catalogBffService.GetItemById(id);
        return Ok(item);
    }
}
