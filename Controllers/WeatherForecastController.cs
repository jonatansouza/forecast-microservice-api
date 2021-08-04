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
        [Route("{country}/{city}")]
        public async Task<IActionResult> Get(string country, string city)
        {
            var tip = new
            {
                Example = "country = BR, city = Rio de Janeiro"
            };
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(city) || country.Length != 2)
            {
                return BadRequest(tip);
            }
            try
            {
                var forecast = await _client.GetCurrentWeatherAsync(country, city);
                return Ok(new WeatherForecast
                {
                    Summary = forecast.weather[0].description,
                    TemperatureC = (int)forecast.main.temp,
                    Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime,
                    NameFound = forecast.name,
                    SearchTerm = "country - " + country + ", city - " + city
                });
            }
            catch (Exception e)
            {
                return NotFound(tip);
            }
        }
    }
}
