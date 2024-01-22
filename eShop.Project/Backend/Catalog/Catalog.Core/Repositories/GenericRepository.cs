namespace Catalog.Core.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly CatalogDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> Get(int page, int size)
    {
        var entities = await _dbSet
            .AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .OrderBy(unit => unit.Id)
            .ToListAsync();
        
        return entities;
    }

    public virtual async Task<TEntity> GetById(int id)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        
        return entity;
    }

    public virtual async Task<int> Add(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        
        return entity.Id;
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public virtual async Task<int> Delete(int id)
    {
        var entity = await _dbContext.Set<TEntity>()
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public virtual async Task<int> Count()
    {
        return await _dbSet.CountAsync();
    }
}
