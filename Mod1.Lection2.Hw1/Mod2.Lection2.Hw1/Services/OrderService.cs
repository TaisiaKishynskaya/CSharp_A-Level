using Mod2.Lection2.Hw1.Models;
using Mod2.Lection2.Hw1.Repositories;

namespace Mod2.Lection2.Hw1.Services;

internal class OrderService
{
    private readonly OrderRepository _orderRepository; // Здесь сохраняем экземпляр CartRepository
    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository; // Принимаем переданный экземпляр CartRepository
    }

    public void CreateOrder(Order order)
    {

    }

    public void PrintOrder(Order order)
    {

    }
}
