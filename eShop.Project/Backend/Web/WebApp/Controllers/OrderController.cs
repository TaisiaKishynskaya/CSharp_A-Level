using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Abstractions;

namespace WebApp.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
    {
        var userId = _orderService.FindUserId(HttpContext);
        orderRequest.UserId = userId;
        var addedOrder = await _orderService.AddOrder(orderRequest);
        return RedirectToAction("Account", "Account");
    }

    public IActionResult OrderForm()
    {
        return View();
    }
}
