using Mod2.Lection2.Hw1.Models;
using Mod2.Lection2.Hw1.Repositories;
using Mod2.Lection2.Hw1.Services;

namespace Mod2.Lection2.Hw1
{
    internal class Program
    {
        static void Main()
        {
            var product = new Product();
            var productService = new ProductService();

            productService.PrintDictionaryOfAllProducts(product);


            var cartRepository = new CartRepository(product); // Передаем его в CartRepository
            var cart = new Cart();

            var cartService = new CartService(cartRepository); // Создаем экземпляр CartService, передаем ему cartRepository
            cartService.PrintCartDictionary(cart); // Вызываем метод PrintCartDictionary, передавая cart*/


            var orderRepository = new OrderRepository(cart);
            var order = new Order();

            var orderService = new OrderService(orderRepository);

        }
    }
}