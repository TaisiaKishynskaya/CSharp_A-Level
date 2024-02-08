using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var client = _httpClientFactory.CreateClient();

            var discoveryDocument = await _loginService.GetDiscoveryDocumentAsync(client);

            if (discoveryDocument.IsError)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var tokenResponse = await _loginService.RequestPasswordTokenAsync(client, discoveryDocument, model);

            if (tokenResponse.IsError)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var userInfoResponse = await _loginService.GetUserInfoAsync(client, discoveryDocument, tokenResponse);

            if (userInfoResponse.IsError)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            await _loginService.SignInAsync(HttpContext, userInfoResponse, tokenResponse);

            return RedirectToAction("Index", "Home");
        }
    }
}