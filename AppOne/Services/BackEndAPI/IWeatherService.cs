using AppOne.Models.Api;

namespace AppOne.Services.BackEndAPI
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherService();
    }
}
