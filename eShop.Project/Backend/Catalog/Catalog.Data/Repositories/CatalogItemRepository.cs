namespace Catalog.DataAccess.Repositories;

public class CatalogItemRepository : ICatalogItemRepository<CatalogItemEntity>
{
    private readonly CatalogDbContext _dbContext;

    public CatalogItemRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CatalogItemEntity>> Get(int page, int size)
    {
        return await _dbContext.Items
            .Include(item => item.Type)
            .Include(item => item.Brand)
            .AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .OrderBy(unit => unit.Id)
            .ToListAsync();

    }

    public async Task<CatalogItemEntity> GetById(int id)
    {
        return await _dbContext.Items
            .Include(item => item.Type)
            .Include(item => item.Brand)
            .FirstOrDefaultAsync(item => item.Id == id);
    }
    public async Task<int> Add(CatalogItemEntity item)
    {
        await _dbContext.Items.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int> Update(CatalogItemEntity item)
    {
        _dbContext.Items.Update(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int> Delete(int id)
    {
        var item = await _dbContext.Items.FindAsync(id);
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
        return id;
    }
    public async Task<int> Count()
    {
        return await _dbContext.Items.CountAsync();
    }

    public async Task<CatalogItemEntity> GetByTitle(string title)
    {
        return await _dbContext.Items.FirstOrDefaultAsync(item => item.Title == title);
    }

    public async Task<CatalogItemEntity> GetByPictureFile(string pictureFile)
    {
        return await _dbContext.Items
            .Include(item => item.Type)
            .Include(item => item.Brand)
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.PictureFile == pictureFile);
    }

}
