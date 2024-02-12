using BackEndAPITwo.Model;

namespace BackEndAPITwo.Services
{
    public interface ISendEmailService
    {
        Task SendEmail(WeatherForecast weatherForecast);
    }
}
