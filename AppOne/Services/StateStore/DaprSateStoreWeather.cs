using AppOne.Models.Api;
using AppOne.Services.BackEndAPIOne;
using Dapr.Client;

namespace AppOne.Services.StateStore
{
    public class DaprSateStoreWeather: IStateStoreWeatherService
    {
        private readonly DaprClient _daprClient;
        private ILogger<DaprSateStoreWeather> _logger;
        private const string stateStoreName = "statestore";
        private const string key = "weatherList";

        public DaprSateStoreWeather(ILogger<DaprSateStoreWeather> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastList()
        {
     
            return await GetWeatherListFromStore();
        }
        public async Task SaveWeatherForecastList(List<WeatherForecast> weather)
        {
              await SaveToStore(weather);
        }
        private async Task SaveToStore(List<WeatherForecast> weather)
        {
             _logger.LogInformation("Reset stateStore");
             await _daprClient.DeleteStateAsync(stateStoreName, key);
             await _daprClient.SaveStateAsync(stateStoreName, key, weather);
        }
        private async Task<List<WeatherForecast>> GetWeatherListFromStore()
        {  
            _logger.LogInformation("Get weather list in the state store");
            List<WeatherForecast> wList = await _daprClient.GetStateAsync<List<WeatherForecast>>(stateStoreName, key);
           
            return wList;

        }

    }
}
