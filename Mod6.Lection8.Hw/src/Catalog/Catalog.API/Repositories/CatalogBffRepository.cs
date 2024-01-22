using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories;

public class CatalogBffRepository : ICatalogBffRepository
{
    private readonly CatalogDbContext _dbContext;
    private readonly ILogger<CatalogBffRepository> _logger;

    public CatalogBffRepository(
        CatalogDbContext dbContext,
        ILogger<CatalogBffRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogType>> GetTypes()
    {
        return await _dbContext.CatalogTypes
            .OrderBy(type => type.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<CatalogBrand>> GetBrands()
    {
        return await _dbContext.CatalogBrands
            .OrderBy(brand => brand.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<CatalogItem>> GetItems()
    {
        return await _dbContext.CatalogItems
            .Include(item => item.CatalogType)
            .Include(item => item.CatalogBrand)
            .OrderBy(item => item.Id)
            .ToListAsync();
    }

}

