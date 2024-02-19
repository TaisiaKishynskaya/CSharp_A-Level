namespace Catalog.DataAccess.Repositories;

// Этот код представляет собой реализацию репозитория для сущности CatalogBrandEntity. 

public class CatalogBrandRepository : ICatalogBrandRepository<CatalogBrandEntity>
{
    private readonly CatalogDbContext _dbContext; //  Это поле класса, которое содержит экземпляр контекста БД CatalogDbContext.

    // Это конструктор класса, который принимает CatalogDbContext в качестве параметра и сохраняет его в поле _dbContext.
    public CatalogBrandRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Этот метод выполняет запрос (с помощью EF Core) к БД для получения страницы сущностей CatalogBrandEntity
    public async Task<IEnumerable<CatalogBrandEntity>> Get(int page, int size)
    {
        return await _dbContext.Brands
            .AsNoTracking() // используется для указания, что результаты запроса не должны быть отслеживаемыми.
            .OrderBy(brand => brand.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(); // асинхронно выполняет запрос и возвращает список результатов.
    }

    // Этот метод получает сущность CatalogBrandEntity по ее идентификатору (ID).
    public async Task<CatalogBrandEntity> GetById(int id)
    {
        return await _dbContext.Brands.FirstOrDefaultAsync(brand => brand.Id == id);
    }

    // Этот метод добавляет новую сущность CatalogBrandEntity в контекст БД и сохраняет изменения.
    public async Task<int> Add(CatalogBrandEntity brand)
    {
        await _dbContext.Brands.AddAsync(brand);
        await _dbContext.SaveChangesAsync();
        return brand.Id;
    }

    // Этот метод обновляет существующую сущность CatalogBrandEntity в контексте БД и сохраняет изменения.
    public async Task<int> Update(CatalogBrandEntity brand)
    {
        _dbContext.Brands.Update(brand);
        await _dbContext.SaveChangesAsync();
        return brand.Id;
    }

    // Этот метод удаляет сущность CatalogBrandEntity по ее идентификатору (ID) из контекста БД и сохраняет изменения.
    public async Task<int> Delete(int id)
    {
        var brand = await _dbContext.Brands.FindAsync(id);
        _dbContext.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return id;
    }
    
    // Этот метод возвращает количество сущностей CatalogBrandEntity в БД.
    public async Task<int> Count()
    {
        return await _dbContext.Brands.CountAsync();
    }

    //  Этот метод получает сущность CatalogBrandEntity по ее названию (title).
    public async Task<CatalogBrandEntity> GetByTitle(string title)
    {
        return await _dbContext.Brands.FirstOrDefaultAsync(brand => brand.Title == title);
    }
}
