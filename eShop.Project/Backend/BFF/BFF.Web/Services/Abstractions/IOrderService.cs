using BFF.Web.Responses;

namespace BFF.Web.Services.Abstractions;

public interface IOrderService
{
    Task<Order> GetOrderById(int id);
    Task<IEnumerable<Order>> GetOrdersByUser(string userId, int page, int size);
    Task<IEnumerable<Order>> GetOrders(int page, int size);
    Task<Order> AddOrder(OrderRequest orderRequest);
    Task<Order> DeleteOrder(int id);
}