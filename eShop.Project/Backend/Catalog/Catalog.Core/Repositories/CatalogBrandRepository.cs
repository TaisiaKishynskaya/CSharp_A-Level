namespace Catalog.Core.Repositories;

public class CatalogBrandRepository : GenericRepository<CatalogBrandEntity>, ICatalogBrandRepository
{
    public CatalogBrandRepository(CatalogDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<CatalogBrandEntity> Update(CatalogBrandEntity entity)
    {
        await _dbContext.Brands
            .Where(brand => brand.Id == entity.Id)
            .ExecuteUpdateAsync(sp => sp
            .SetProperty(p => p.Title, p => entity.Title)
            .SetProperty(p => p.UpdatedAt, p => DateTime.UtcNow));

        var updatedEntity = await _dbContext.Brands.FirstOrDefaultAsync(brand => brand.Id == entity.Id);

        return updatedEntity;
    }
}
