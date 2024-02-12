namespace BFF.Web.Services.Abstractions;

public interface IOrderBffService
{
    Task<OrderResponse> GetOrderById(int id);
    Task<IEnumerable<OrderResponse>> GetOrdersByUser(string userId, int page, int size);
    Task<IEnumerable<OrderResponse>> GetOrders(int page, int size);
    Task<OrderResponse> AddOrder(OrderRequest orderRequest);
    Task<OrderResponse> DeleteOrder(int id);
}