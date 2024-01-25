namespace Catalog.Core.Repositories;

public class CatalogItemRepository : GenericRepository<CatalogItemEntity>, ICatalogItemRepository
{
    public CatalogItemRepository(CatalogDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<CatalogItemEntity>> Get(int page, int size)
    {
        var entities = await _dbContext.Items
            .Include(item => item.Type)
            .Include(item => item.Brand)
            .AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .OrderBy(unit => unit.Id)
            .ToListAsync();

        return entities;
    }

    public override async Task<CatalogItemEntity> GetById(int id)
    {
        var entity = await _dbContext.Items
            .AsNoTracking()
            .Include(item => item.Type)
            .Include(item => item.Brand)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        return entity;
    }

    public override async Task<int> Add(CatalogItemEntity entity)
    {
        var typeEntity = await _dbContext.Types.FirstOrDefaultAsync(t => t.Title == entity.Type.Title);
        var brandEntity = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Title == entity.Brand.Title);

        entity.Type = typeEntity;
        entity.Brand = brandEntity;

        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public override async Task<CatalogItemEntity> Update(CatalogItemEntity entity)
    {
        var typeEntity = await _dbContext.Types.FirstOrDefaultAsync(t => t.Title == entity.Type.Title);
        var brandEntity = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Title == entity.Brand.Title);

        entity.TypeId = typeEntity?.Id;
        entity.BrandId = brandEntity?.Id;

        await _dbContext.Items
            .Where(item => item.Id == entity.Id)
            .ExecuteUpdateAsync(sp => sp
            .SetProperty(p => p.Title, p => entity.Title)
            .SetProperty(p => p.Description, p => entity.Description)
            .SetProperty(p => p.Price, p => entity.Price)
            .SetProperty(p => p.PictureFile, p => entity.PictureFile)
            .SetProperty(p => p.TypeId, p => entity.TypeId)
            .SetProperty(p => p.BrandId, p => entity.BrandId)
            .SetProperty(p => p.UpdatedAt, p => DateTime.UtcNow));

        var updatedEntity = await GetById(entity.Id);

        return updatedEntity;
    }
}
