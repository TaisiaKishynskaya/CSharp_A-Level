namespace Catalog.Core.Abstractions.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> Get(int page, int size);
    Task<T> GetById(int id);
    Task<int> Add(T entity);
    Task<int> Update(T entity);
    Task<int> Delete(int id);
    Task<int> Count();
}
