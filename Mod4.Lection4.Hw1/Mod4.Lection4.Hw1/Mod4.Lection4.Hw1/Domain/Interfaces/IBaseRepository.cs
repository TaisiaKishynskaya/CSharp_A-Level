using System.Linq.Expressions;

namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task CreateAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
    IQueryable<T> FindAllAsync();
    IQueryable<T> FindByConditionAsync(Expression<Func<T, bool>> expression);
}
