namespace Catalog.DataAccess.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository<CatalogBrandEntity>
{
    private readonly CatalogDbContext _dbContext;

    public CatalogBrandRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CatalogBrandEntity>> Get(int page, int size)
    {
        return await _dbContext.Brands
            .AsNoTracking()
            .OrderBy(brand => brand.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }

    public async Task<CatalogBrandEntity> GetById(int id)
    {
        return await _dbContext.Brands.FirstOrDefaultAsync(brand => brand.Id == id);
    }

    public async Task<int> Add(CatalogBrandEntity brand)
    {
        await _dbContext.Brands.AddAsync(brand);
        await _dbContext.SaveChangesAsync();
        return brand.Id;
    }

    public async Task<int> Update(CatalogBrandEntity brand)
    {
        _dbContext.Brands.Update(brand);
        await _dbContext.SaveChangesAsync();
        return brand.Id;
    }

    public async Task<int> Delete(int id)
    {
        var brand = await _dbContext.Brands.FindAsync(id);
        _dbContext.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return id;
    }

    public async Task<int> Count()
    {
        return await _dbContext.Brands.CountAsync();
    }

    public async Task<CatalogBrandEntity> GetByTitle(string title)
    {
        return await _dbContext.Brands.FirstOrDefaultAsync(brand => brand.Title == title);
    }
}






