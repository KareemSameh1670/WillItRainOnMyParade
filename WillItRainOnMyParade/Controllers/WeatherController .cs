using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WillItRainOnMyParade.BLL.DTOs;
using WillItRainOnMyParade.BLL.Interfaces;

namespace WillItRainOnMyParade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{lat:float}/{lon:float}/{date:datetime}")]
        public async Task<ActionResult<WeatherPredictionResult>> GetDailyMean(float lat, float lon, DateTime date, [FromQuery] int NumOfYears=10)
        {
            var result = await _weatherService.GetDailyProbabilities(lat, lon, date, NumOfYears);
            if (result == null)
                return NotFound("Could not fetch weather data from NASA API.");

            return Ok(result);
        }
    
    }
}
