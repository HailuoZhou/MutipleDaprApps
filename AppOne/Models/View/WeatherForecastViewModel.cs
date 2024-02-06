using AppOne.Models.Api;

namespace AppOne.Models.View
{
    public class WeatherForecastViewModel
    {
       public IEnumerable<WeatherForecast> WeatherForecast { get; set; } =  new List<WeatherForecast>();  
    }
}
