using Basket.Host.Models;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services
{
    public class ItemService : IItemService
    {
        private static readonly List<Item> _items = new List<Item>()
        {
             new Item() { Title = "Item1", Description = "1", Price = 10.0m },
             new Item() { Title = "Item2", Description = "2", Price = 20.0m }
        };

        public List<Item> Get()
        {
            return _items;
        }

        public int Add(Item item)
        {
            _items.Add(item);
            return item.Id;
        }
    }
}
