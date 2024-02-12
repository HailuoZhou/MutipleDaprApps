using BackEndAPITwo.Model;
using Dapr.Client;


namespace BackEndAPITwo.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<SendEmailService> _logger;
        public SendEmailService(DaprClient daprClient, ILogger<SendEmailService> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
                
        }

        public async Task SendEmail(WeatherForecast weatherForecast)
        {
             _logger.LogInformation($"Sending email");
            var metadata = new Dictionary<string, string>
            {
                ["emailFrom"] = "noreply@weatherservice.com",
                ["emailTo"] = "tester@gmail.com",
                ["subject"] = $"Thank you for subscribing weather"
            };
            var weatherstring = $"{weatherForecast.Date.ToShortDateString()} - {weatherForecast.Summary}";  
            var body = $"<h2>Weather Forecast</h2><p>{weatherstring}</p>";
            await _daprClient.InvokeBindingAsync("sendemail", "create", body, metadata);

        }

    }
}
