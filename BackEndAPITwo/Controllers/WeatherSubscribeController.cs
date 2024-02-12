using BackEndAPITwo.Model;
using BackEndAPITwo.Services;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPITwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherSubscribeController : ControllerBase
    {
        private readonly ILogger<WeatherSubscribeController> _logger;
        private readonly ISendEmailService _sendEmailService;

        public WeatherSubscribeController(ILogger<WeatherSubscribeController> logger, ISendEmailService sendEmailService)
        {
               _logger = logger;
               _sendEmailService = sendEmailService;
        }

   
        [HttpPost]
        [Topic("pubsub", "weather")]
        public async Task<IActionResult> Send([FromBody]WeatherForecast weatherForecast)
        {
            _logger.LogInformation($"Received weather forecast info {weatherForecast.Date}");
             await _sendEmailService.SendEmail(weatherForecast);

            return Ok();

        }
    }
}
