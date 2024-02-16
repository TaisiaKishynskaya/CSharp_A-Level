namespace WebApp.Services.Abstractions;

public interface IOrderService
{
    string FindUserId(HttpContext httpContext);
    Task<IEnumerable<OrderModel>> GetOrdersByUser(HttpContext httpContext);
    Task<OrderModel> AddOrder(OrderRequest orderRequest);
}