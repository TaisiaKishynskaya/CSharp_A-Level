using Mod2.Lection2.Hw1.Models;
using Mod2.Lection2.Hw1.Repositories;
using Mod2.Lection2.Hw1.Services;

namespace Mod2.Lection2.Hw1;

internal class Starter
{
    public void Start()
    {   
        Console.WriteLine("First, you need to create a product list in shop!");

        var product = new Product();
        var productService = new ProductService();
        productService.PrintDictionaryOfAllProducts(product);

        var cartRepository = new CartRepository(product);  // Передаем его в CartRepository
        var cart = new Cart();
        var cartService = new CartService(cartRepository);  // Создаем экземпляр CartService, передаем ему cartRepository

        var orderRepository = new OrderRepository(cart);
        var order = new Order();
        var orderService = new OrderService(orderRepository);

        while (true)
        {
            Console.WriteLine("Have you already added product to cart? (y/n or e to stop)");
            var input = Validation.ValudateAnswer();

            switch (input)
            {
                case "n":
                    Console.WriteLine("Time to shop! Press Enter.");
                    cartService.PrintCartDictionary(cart);  // Вызываем метод PrintCartDictionary, передавая cart
                    break;

                case "y":
                    Console.WriteLine("Do you ready for making order? (y/n or e to stop)");
                    var answer = Validation.ValudateAnswer();

                    switch (answer)
                    {
                        case "y":
                            orderService.PrintOrder(order);
                            break;
                        case "n":
                            cartService.PrintCartDictionary(cart);
                            break;
                        case "e":
                            return;
                    }

                    break;

                case "e":
                    return;
            }
        }
    }
}
