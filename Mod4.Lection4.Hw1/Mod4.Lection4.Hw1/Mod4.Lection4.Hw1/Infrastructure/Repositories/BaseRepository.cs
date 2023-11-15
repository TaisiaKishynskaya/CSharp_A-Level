using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Infrastructure.Context;
using System.Linq.Expressions;

namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly EFCoreContext _context;
    public BaseRepository(EFCoreContext context) => _context = context;
    
    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<T> FindAllAsync() => _context.Set<T>().AsNoTracking();

    public IQueryable<T> FindByConditionAsync(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
