using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
