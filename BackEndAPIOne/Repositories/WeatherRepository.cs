using BackEndAPIOne.Model;
using System;
using System.ComponentModel;

namespace BackEndAPIOne.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private List<WeatherForecast> _tempData= new List<WeatherForecast>();
        private readonly ILogger<WeatherRepository> _logger;
        public WeatherRepository(ILogger<WeatherRepository> logger)
        {
            _logger = logger;
                
        }
        public async Task<IEnumerable<WeatherForecast>> GetData()
        {
            return await Task.Run(() => LoadTempData());
        }

        private IEnumerable<WeatherForecast> LoadTempData()
        {
            _logger.LogInformation("Load data");
           
             return LoadInitialTempData().ToList();
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public void UpdateData()
        {
            _logger.LogInformation("Scheduled to call update data");
            _tempData = LoadInitialTempData().ToList();

            var dataCount = _tempData.Count;

            _tempData.Add(new WeatherForecast {

                Id = dataCount,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(dataCount)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]

            });
            _logger.LogInformation($"the weather counts: {_tempData.Count}");
        }

        private static WeatherForecast[] LoadInitialTempData()
        {
            return  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
           .ToArray();
   
        }
    }
}
