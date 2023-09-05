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

    private static int OrderCounter = 1; // Статическая переменная для счетчика заказов

    public void PrintOrder(Order order)
    {
        _orderRepository.AddProductsFromCartInOrder(order);

        Console.WriteLine("All products in order:");
        foreach (var opr in order.OrderProducts)
        {
            Console.WriteLine($"{opr.Key} cost {opr.Value}$");
        }

        CountOrderNumber(order);
    }

    public void CountOrderNumber(Order order)
    {
        order.Number = OrderCounter;  // Устанавливаем номер заказа перед печатью
        Console.WriteLine($"Order #{order.Number} has been successfully placed!");
        OrderCounter++;  // Увеличиваем счетчик для следующего заказа
    } 
}
