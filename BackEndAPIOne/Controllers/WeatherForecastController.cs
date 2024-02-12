using BackEndAPIOne.Model;
using BackEndAPIOne.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPIOne.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherRepository weatherRepository)
        {
            _logger = logger;
            _weatherRepository = weatherRepository;
        }
             
        [HttpGet(Name = "GetWeatherForecast")]
        public Task<IEnumerable<WeatherForecast>> Get()
        {
             return _weatherRepository.GetData();
        }

    }
}
