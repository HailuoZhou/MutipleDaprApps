using AppOne.Models;
using AppOne.Models.Api;
using AppOne.Models.View;
using AppOne.Services.BackEndAPI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            var WeatherValue =  _weatherService.GetWeatherService();
            var result = WeatherValue.Result;

            var viewModel = new WeatherForecastViewModel
            {
                WeatherForecast = result
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
