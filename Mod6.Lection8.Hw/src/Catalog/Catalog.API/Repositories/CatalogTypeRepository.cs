namespace Catalog.API.Repositories;
using Catalog.API.Data.Entities;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(
        CatalogDbContext dbContext,
        ILogger<CatalogTypeRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogType>> Get()
    {
        return await _dbContext.CatalogTypes.AsNoTracking().OrderBy(type => type.Id).ToListAsync();
    }

    public async Task<int> Add(CatalogType catalogType)
    {
        _dbContext.CatalogTypes.Add(catalogType); 

        await _dbContext.SaveChangesAsync();

        return catalogType.Id;
    }

    public async Task Update(CatalogType catalogType)
    {
        var existingCatalogType = await _dbContext.CatalogTypes.FirstOrDefaultAsync(type => type.Id == catalogType.Id);

        existingCatalogType.Type = catalogType.Type;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existingCatalogType = await _dbContext.CatalogTypes.FirstOrDefaultAsync(type => type.Id == id);

        _dbContext.CatalogTypes.Remove(existingCatalogType);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<CatalogType> GetByName(string typeName)
    {
        return await _dbContext.CatalogTypes.FirstOrDefaultAsync(type => type.Type == typeName);
    }

    public async Task<CatalogType> GetById(int id)
    {
        return await _dbContext.CatalogTypes.FirstOrDefaultAsync(type => type.Id == id);
    }


}

