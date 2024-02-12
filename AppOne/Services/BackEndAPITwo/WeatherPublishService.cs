using AppOne.Models.Api;
using AppOne.Models.View;
using AppOne.Services.BackEndAPIOne;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace AppOne.Services.BackEndAPITwo
{
    public class WeatherPublishService: IWeatherPublishService 
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<WeatherPublishService> _logger;

        public WeatherPublishService(ILogger<WeatherPublishService> logger, DaprClient daprClient)
        {
             _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<string> PublishWeather(WeatherForecast weatherForecast)
        {
           
            _logger.LogInformation("Publishing weather event by Dapr pubsub");

            var publishString = $"{weatherForecast.Date.ToShortDateString()} - {weatherForecast.Summary}";

            await _daprClient.PublishEventAsync("pubsub", "weather", weatherForecast);
                   
            return publishString;
        }
        
    }
}
