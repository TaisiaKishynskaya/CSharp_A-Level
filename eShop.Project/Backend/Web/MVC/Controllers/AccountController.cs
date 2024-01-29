using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Account()
        {
            ViewData["Layout"] = "~/Views/Shared/_AccountLayout.cshtml";
            return View();
        }
    }
}
