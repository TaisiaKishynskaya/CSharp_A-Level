using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Controllers;

public class WeatherForecastController : Controller
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [Authorize]
    public async Task<IActionResult> WeatherForecast()
    {
        try
        {
            var weatherForecasts = await _weatherForecastService.GetWeatherForecast();
            return View(weatherForecasts);
        }
        catch (Exception)
        {
            return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

