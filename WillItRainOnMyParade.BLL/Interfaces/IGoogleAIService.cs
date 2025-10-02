using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillItRainOnMyParade.BLL.DTOs;
using WillItRainOnMyParade.DAL.Clients;

namespace WillItRainOnMyParade.BLL.Interfaces
{
    public interface IGoogleAIService
    {
        public Task<string> GeminiRecommendations(WeatherPredictionResult conditions);
    }
}
