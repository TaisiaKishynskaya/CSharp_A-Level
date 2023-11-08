using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface IOrderItemRepository
{
    Task CreateOrderItemAsync(OrderItem orderitem);
    Task UpdateOrderItemAsync(OrderItem orderitem);
    Task DeleteOrderItemAsync(OrderItem orderitem);
    Task<OrderItem> GetOrderItemAsync(Guid orderitemId);
    Task<IReadOnlyCollection<OrderItem>> GetAllItemsAsync();
}
