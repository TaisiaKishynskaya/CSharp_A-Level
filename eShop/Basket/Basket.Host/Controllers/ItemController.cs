using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "basket.basketitem")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _itemService.Get();
        }

        [HttpPost]
        public IActionResult Add(ItemRequest itemRequest)
        {
            var itemToAdd = new Item
            {
                Title = itemRequest.Title,
                Description = itemRequest.Description,
                Price = itemRequest.Price
            };

            _itemService.Add(itemToAdd);

            return Ok(itemToAdd.Id);
        }

    }
}
