using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers;

public class ErrorController : Controller
{
    public IActionResult Error() => View();
}