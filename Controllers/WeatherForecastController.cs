using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Forecast.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherClient _client;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherClient client)
        {
            _logger = logger;
            _client = client;
        }


        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> Get(string city)
        {
            var forecast = await _client.GetCurrentWeatherAsync(city);

            return new WeatherForecast
            {
                Summary = forecast.weather[0].description,
                TemperatureC = (int)forecast.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime,
                NameFound = forecast.name,
                SearchTerm = city
            };
        }
    }
}
