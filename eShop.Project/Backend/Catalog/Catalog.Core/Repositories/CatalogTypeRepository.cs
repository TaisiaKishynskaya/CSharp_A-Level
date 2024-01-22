namespace Catalog.Core.Repositories;

public class CatalogTypeRepository : GenericRepository<CatalogTypeEntity>, ICatalogTypeRepository
{
    public CatalogTypeRepository(CatalogDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<CatalogTypeEntity> Update(CatalogTypeEntity entity)
    {
        await _dbContext.Types
            .Where(type => type.Id == entity.Id)
            .ExecuteUpdateAsync(sp => sp
            .SetProperty(p => p.Title, p => entity.Title)
            .SetProperty(p => p.UpdatedAt, p => DateTime.UtcNow));

        var updatedEntity = await _dbContext.Types
            .FirstOrDefaultAsync(type => type.Id == entity.Id);

        return updatedEntity;
    }
}
