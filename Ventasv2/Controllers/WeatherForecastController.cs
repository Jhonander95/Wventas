using Microsoft.AspNetCore.Mvc;

namespace Ventasv2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            List<WeatherForecast> lst = new List<WeatherForecast>();
            lst.Add(new WeatherForecast() { Id = 4, Nombre = "Jhonander" });
            lst.Add(new WeatherForecast() { Id = 5, Nombre = "John Doe" });
            return lst;
        }
    }
}