using AppOne.Models;
using AppOne.Models.Api;
using AppOne.Models.View;
using AppOne.Services.BackEndAPIOne;
using AppOne.Services.BackEndAPITwo;
using AppOne.Services.StateStore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IWeatherPublishService _weatherPublishService;
        private readonly IStateStoreWeatherService _stateStoreWeatherService;
        public HomeController(ILogger<HomeController> logger, 
               IWeatherService weatherService,
               IWeatherPublishService weatherPublishService,
               IStateStoreWeatherService stateStoreWeatherService
               )
        {
            _weatherService = weatherService;
            _weatherPublishService = weatherPublishService;
            _stateStoreWeatherService = stateStoreWeatherService;
            _logger = logger;
        }

        public IActionResult Index()
        { 
            var WeatherValue =  _weatherService.GetWeatherService();
            var result = WeatherValue.Result;

            var viewModel = new WeatherForecastViewModel
            {
                WeatherForecast = result
            };

            // store 
            _stateStoreWeatherService.SaveWeatherForecastList(result.ToList());

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

       
        public  async Task<IActionResult> PublishWeather(int Id)
        {
            if (ModelState.IsValid)
            {
                var wList= await _stateStoreWeatherService.GetWeatherForecastList();
                var weather = wList.FirstOrDefault(x=>x.Id == Id);
                
                if (weather != null)
                {
                   var publishedWeatherString = await _weatherPublishService.PublishWeather(weather);
                    TempData["message"] = publishedWeatherString;
                   return RedirectToAction("Confirmation");
                }
                else
                {
                   return View("Error");
                }
            }
            else {

                return View("Index");
            }
        }

        public IActionResult Confirmation()
        {
            var message = TempData["message"] as string;

            ViewBag.Message = message;
            return View();
        }
       
    }
}
