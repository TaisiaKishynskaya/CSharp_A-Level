namespace Catalog.Core.Abstractions.Repositories;

// обобщенным интерфейсом репозитория, который определяет основные методы для работы с данными: получение, добавление, обновление, удаление и подсчет.

public interface IRepository<T> where T : class // Он параметризован типом T, который представляет собой класс сущности.
{
    Task<IEnumerable<T>> Get(int page, int size); // получение коллекции сущностей T с возможностью пагинации.
    Task<T> GetById(int id); // получение сущности T по ее идентификатору.
    Task<int> Add(T entity); // добавление новой сущности T.
    Task<int> Update(T entity); // обновление существующей сущности T.
    Task<int> Delete(int id); // удаление сущности T по ее идентификатору.
    Task<int> Count(); // подсчет общего количества сущностей T.
}
