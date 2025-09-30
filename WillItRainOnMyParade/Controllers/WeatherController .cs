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

        [HttpGet("daily-mean")]
        public async Task<ActionResult<WeatherPredictionResult>> GetDailyMean(
           [FromQuery] double lat,
           [FromQuery] double lon,
           [FromQuery] DateTime date)
        {
            var result = await _weatherService.GetDailyMean(lat, lon, date);
            if (result == null)
                return NotFound("Could not fetch weather data from NASA API.");

            return Ok(result);
        }
    
    }
}
