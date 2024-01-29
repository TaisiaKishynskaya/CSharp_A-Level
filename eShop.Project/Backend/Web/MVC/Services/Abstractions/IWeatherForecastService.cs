using WebApp.Models;

namespace WebApp.Services.Abstractions
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecast();
    }
}