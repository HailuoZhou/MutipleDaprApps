using AppOne.Models.Api;
using AppOne.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace AppOne.Services.BackEndAPITwo
{
    public interface IWeatherPublishService  
    {
        Task<string> PublishWeather(WeatherForecast weatherForecast);
    }
}
