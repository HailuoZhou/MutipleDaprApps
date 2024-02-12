using AppOne.Models.Api;

namespace AppOne.Services.BackEndAPIOne
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherService();
    }
}
