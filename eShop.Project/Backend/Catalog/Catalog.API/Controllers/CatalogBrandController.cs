namespace Catalog.API.Controllers;

[ApiController] // Цей атрибут вказує, що клас є контролером API, і використовує деякі налаштування за замовчуванням для обробки запитів API.
[Route("/api/v1/catalog/brands")] // Атрибути маршрутизації вказують, які URL будуть оброблятися цим контролером. В цьому випадку всі URL, що починаються з /api/v1/catalog/brands, будуть направлені до цього контролера.
[Authorize("CatalogApiScope")] // Атрибут позначає, що цей клас контролера є контролером API. Він включає певні налаштування за замовчуванням, які дозволяють використовувати функціонал ASP.NET Core для обробки запитів API.
public class CatalogBrandController : ControllerBase
{
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly IMapper _mapper;

    public CatalogBrandController( // Конструктор приймає два параметри: ICatalogBrandService і IMapper. 
        ICatalogBrandService catalogBrandService, // це сервіс, який надає методи для взаємодії з брендами каталогу
        IMapper mapper) // це інтерфейс, який використовується для мапування об'єктів між різними типами.
    {
        _catalogBrandService = catalogBrandService;
        _mapper = mapper;
    }

    [HttpGet] // Цей метод обробляє GET-запит і приймає два необов'язкових параметри, які використовуються для пагінації результатів.
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {
        var brands = await _catalogBrandService.Get(page, size); // викликається метод Get сервісу catalogBrandService, що отримує список брендів каталогу з БД за заданими параметрами сторінки і розмір

        var total = await _catalogBrandService.Count(); // викликається метод Count сервісу catalogBrandService, щоб отримати загальну кількість брендів каталогу.
        
        // Загальна кількість використовується для розрахунку кількості сторінок та створення об'єкта PaginatedResponse, який містить інформацію про сторінку і список брендів.
        var paginatedResponse = new PaginatedResponse<CatalogBrand>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            brands
        );

        return Ok(paginatedResponse); // метод повертає успішну відповідь з об'єктом PaginatedResponse у форматі JSON.
    }

    [HttpGet("{id}")] // Цей метод обробляє GET-запит і приймає параметр id, який вказує на ідентифікатор бренду.
    public async Task<IActionResult> GetBrandById(int id)
    {
        var brand = await _catalogBrandService.GetById(id); // Викликається метод GetById сервісу catalogBrandService, що отримує бренд каталогу за його ідентифікатором.
        return Ok(brand); // метод повертає успішну відповідь з об'єктом бренду у форматі JSON.
    }
    
    [HttpPost] // метод обробляє POST-запит і приймає об'єкт CatalogBrandRequest, який містить дані нового бренду
    public async Task<IActionResult> AddBrand([FromBody] CatalogBrandRequest request)
    {
        var brand = _mapper.Map<CatalogBrand>(request); // об'єкт CatalogBrandRequest мапується на об'єкт CatalogBrand за допомогою мапера.
        brand.CreatedAt = DateTime.UtcNow; // встановлюється час створення бренду

        var id = await _catalogBrandService.Add(brand); // викликається метод Add сервісу catalogBrandService, щоб додати новий бренд до БД.

        return Ok(id); // метод повертає успішну відповідь з ідентифікатором нового бренду.
    }

    [HttpPut("{id}")] // обробляє PUT-запит і приймає параметр id для ідентифікації бренду, який потрібно оновити, і об'єкт CatalogBrandRequest з новими даними бренду.
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] CatalogBrandRequest request)
    {
        // Об'єкт CatalogBrandRequest мапується на об'єкт CatalogBrand, де встановлюється ідентифікатор бренду.
        var brand = _mapper.Map<CatalogBrand>(request);
        brand.Id = id;

        await _catalogBrandService.Update(brand); // викликається метод Update сервісу catalogBrandService, який оновлює існуючий бренд в БД.

        var updatedBrand = await _catalogBrandService.GetById(id); // викликається метод GetById для отримання оновленого бренду

        return Ok(updatedBrand); // метод повертає успішну відповідь з цим брендом
    }

    [HttpDelete("{id}")] // обробляє DELETE-запит і приймає параметр id, що вказує на ідентифікатор бренду, який потрібно видалити.
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var result = await _catalogBrandService.Delete(id); // Викликається метод Delete сервісу catalogBrandService, який видаляє бренд з БД.
        return Ok(id); // // метод повертає успішну відповідь
    }
}

