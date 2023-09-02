using Mod2.Lection2.Hw1.Models;

namespace Mod2.Lection2.Hw1.Repositories;

internal class OrderRepository
{
    private readonly Cart _cart; // В конструкторе принимаем заполненный экземпляр Product
    public OrderRepository(Cart cart)
    {
        _cart = cart; // Сохраняем переданный экземпляр Product
    }

    public void AddProductsFromCartInOrder(Order order)
    {
        // Копируем пары ключ-значение из корзины в заказ
        foreach (var kvp in _cart.CartProducts)
        {
            order.OrderProducts[kvp.Key] = kvp.Value;
        }
    }
}
