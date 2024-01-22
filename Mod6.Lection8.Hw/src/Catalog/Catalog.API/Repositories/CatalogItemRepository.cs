using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly CatalogDbContext _dBcontext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        CatalogDbContext context,
        ILogger<CatalogItemRepository> logger)
    {
        _dBcontext = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogItem>> Get()
    {
        return await _dBcontext.CatalogItems
            .Include(item => item.CatalogType)
            .Include(item => item.CatalogBrand)
            .AsNoTracking()
            .OrderBy(item => item.Id)
            .ToListAsync();
    }

    public async Task<CatalogItem?> GetById(int id)
    {
        return await _dBcontext.CatalogItems
            .Include(item => item.CatalogType)
            .Include(item => item.CatalogBrand)
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<string?> GetPictureUriById(int id)
    {
        var item = await _dBcontext.CatalogItems
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);

        return item.PictureUri;
    }

    public async Task<int> Add(CatalogItem catalogItem)
    {
        _dBcontext.CatalogItems.Add(catalogItem);

        await _dBcontext.SaveChangesAsync();

        return catalogItem.Id;
    }

    public async Task Update(CatalogItem catalogItem)
    {
        var existingCatalogItem = await GetById(catalogItem.Id);

        if (existingCatalogItem != null)
        {
            existingCatalogItem.Name = catalogItem.Name;
            existingCatalogItem.Description = catalogItem.Description;
            existingCatalogItem.Price = catalogItem.Price;
            existingCatalogItem.PictureUri = catalogItem.PictureUri;
            existingCatalogItem.CatalogTypeId = catalogItem.CatalogTypeId;
            existingCatalogItem.CatalogBrandId = catalogItem.CatalogBrandId;
            existingCatalogItem.AvailableStock = catalogItem.AvailableStock;

            await _dBcontext.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var existingCatalogItem = await _dBcontext.CatalogItems.FirstOrDefaultAsync(item => item.Id == id);

        _dBcontext.CatalogItems.Remove(existingCatalogItem);

        await _dBcontext.SaveChangesAsync();
    }


}

