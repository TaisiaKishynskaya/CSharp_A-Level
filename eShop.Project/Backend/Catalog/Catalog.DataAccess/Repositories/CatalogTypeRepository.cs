namespace Catalog.DataAccess.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository<CatalogTypeEntity>
{
    private readonly CatalogDbContext _dbContext;

    public CatalogTypeRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CatalogTypeEntity>> Get(int page, int size)
    {
        return await _dbContext.Types
            .AsNoTracking()
            .OrderBy(type => type.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }

    public async Task<CatalogTypeEntity> GetById(int id)
    {
        return await _dbContext.Types.FirstOrDefaultAsync(type => type.Id == id);
    }

    public async Task<int> Add(CatalogTypeEntity type)
    {
        await _dbContext.Types.AddAsync(type);
        await _dbContext.SaveChangesAsync();
        return type.Id;
    }

    public async Task<int> Update(CatalogTypeEntity type)
    {
        _dbContext.Types.Update(type);
        await _dbContext.SaveChangesAsync();
        return type.Id;
    }

    public async Task<int> Delete(int id)
    {
        var type = await _dbContext.Types.FindAsync(id);
        _dbContext.Remove(type);
        await _dbContext.SaveChangesAsync();
        return id;
    }

    public async Task<int> Count()
    {
        return await _dbContext.Types.CountAsync();
    }

    public async Task<CatalogTypeEntity> GetByTitle(string title)
    {
        return await _dbContext.Types.FirstOrDefaultAsync(type => type.Title == title);
    }
}
