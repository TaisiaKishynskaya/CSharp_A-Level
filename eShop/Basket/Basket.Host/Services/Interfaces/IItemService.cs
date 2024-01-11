using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces
{
    public interface IItemService
    {
        List<Item> Get();
        int Add(Item item);
    }
}
