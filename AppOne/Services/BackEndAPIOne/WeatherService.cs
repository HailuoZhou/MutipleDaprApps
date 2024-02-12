using AppOne.Models.Api;
using System.Text.Json;


namespace AppOne.Services.BackEndAPIOne
{

    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;
        public WeatherService(HttpClient client)
        {
             _client = client;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherService()
        {
            var response = await _client.GetAsync("WeatherForecast");
            
            var dataAsString= await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            
            return JsonSerializer.Deserialize<List<WeatherForecast>>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            
        }
    }
}
