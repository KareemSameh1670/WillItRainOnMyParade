using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI;
using WillItRainOnMyParade.BLL.DTOs;
using WillItRainOnMyParade.BLL.Interfaces;

namespace WillItRainOnMyParade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAIController : ControllerBase
    {
        private readonly IGoogleAIService googleAIService;

        public GoogleAIController(IGoogleAIService googleAIService)
        {
            this.googleAIService = googleAIService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Ask([FromQuery]WeatherPredictionResult predictionResult)
        {
            var result = await googleAIService.GeminiRecommendations(predictionResult);
            return result;
        }
    }

}
