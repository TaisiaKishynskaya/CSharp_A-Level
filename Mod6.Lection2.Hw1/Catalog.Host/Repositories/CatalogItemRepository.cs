using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(CreateProductRequest catalog)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalog.CatalogBrandId,
            CatalogTypeId = catalog.CatalogTypeId,
            Description = catalog.Description,
            Name = catalog.Name,
            PictureFileName = catalog.PictureFileName,
            Price = catalog.Price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem?> GetByIdAsync(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(i => i.Id == id);

        return item;
    }

    public async Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brand)
    {
        var items = await _dbContext.CatalogItems.Where(i => i.CatalogBrand.Brand == brand).ToListAsync();

        return items;
    }

    public async Task<IEnumerable<CatalogItem>> GetByTypeAsync(string type)
    {
        var items = await _dbContext.CatalogItems.Where(i => i.CatalogType.Type == type).ToListAsync();

        return items;
    }
}
