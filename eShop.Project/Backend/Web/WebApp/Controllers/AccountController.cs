using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Abstractions;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly IOrderService _orderService;

    public AccountController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<IActionResult> Account()
    {
        ViewData["Layout"] = "~/Views/Shared/_AccountLayout.cshtml";
        var orders = await _orderService.GetOrdersByUser(HttpContext);
        orders = orders.OrderByDescending(order => order.OrderDate).ToList();
        return View(orders);
    }
}
