using Mod4.Lection4.Hw1.Domain.Models;
using System.Security.Claims;

namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface IOrderRepository
{
    Task CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Order order);
    Task<Order> GetOrderAsync(Guid orderId);
    Task<IReadOnlyCollection<Order>> GetAllOrdersAsync();
    Task AddItems(Guid id, ICollection<OrderItem> orderitems);
}
