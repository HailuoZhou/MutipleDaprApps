using BackEndAPIOne.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPIOne.Controllers
{

    [ApiController]
    [Route("scheduled")]
    public class CronServiceController : ControllerBase
    {
        private readonly ILogger<CronServiceController> _logger;
        private readonly IWeatherRepository _weatherRepository;

        public CronServiceController(ILogger<CronServiceController> logger, IWeatherRepository weatherRepository)
        {
            _logger = logger;
            _weatherRepository = weatherRepository;
        }


        [HttpPost]
        public void OnSchedule()
        {
            _logger.LogInformation("Update Weather Forecast Data");
            _weatherRepository.UpdateData();
        }
    }
}
