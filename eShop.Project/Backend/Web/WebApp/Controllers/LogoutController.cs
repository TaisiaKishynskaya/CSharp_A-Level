namespace WebApp.Controllers;

public class LogoutController : Controller
{
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Catalog", "Catalog");
    }
}
