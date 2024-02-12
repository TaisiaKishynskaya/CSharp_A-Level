namespace Ordering.Core.Abstractions.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> Get(int page, int size);
    Task<Order> GeyById(int id);
    Task<IEnumerable<Order>> GetByUser(string userId, int page, int size);
    Task<Order> Add(Order order, string userId);
    Task<Order> Update(Order order, ClaimsPrincipal userClaims);
    Task<Order> Delete(int id);
}