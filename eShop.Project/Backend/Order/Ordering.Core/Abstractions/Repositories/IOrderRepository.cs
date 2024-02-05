namespace Ordering.Core.Abstractions.Repositories;

public interface IOrderRepository<OrderEntity>
{
    Task<OrderEntity> Add(OrderEntity order);
    Task<OrderEntity> Delete(int id);
    Task<IEnumerable<OrderEntity>> Get(int page, int size);
    Task<OrderEntity> GetById(int id);
    Task<IEnumerable<OrderEntity>> GetByUser(string userId, int page, int size);
    Task<OrderEntity> Update(OrderEntity order);
}