using AppOne.Models.Api;
 
namespace AppOne.Services.StateStore
{
    public interface IStateStoreWeatherService
    {
        Task<List<WeatherForecast>> GetWeatherForecastList();
        Task SaveWeatherForecastList(List<WeatherForecast> weather);
    }
}
