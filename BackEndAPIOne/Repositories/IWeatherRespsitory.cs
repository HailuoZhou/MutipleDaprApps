using BackEndAPIOne.Model;

namespace BackEndAPIOne.Repositories
{
    public interface IWeatherRepository
    {
       Task<IEnumerable<WeatherForecast>> GetData();
       void UpdateData();
    }
}
