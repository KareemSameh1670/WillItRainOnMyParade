using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillItRainOnMyParade.BLL.DTOs;
using WillItRainOnMyParade.BLL.Interfaces;
using WillItRainOnMyParade.DAL.Clients;

namespace WillItRainOnMyParade.BLL.Services
{
    public class GoogleAIService: IGoogleAIService
    {
        private readonly IGoogleAIClient googleAIClient;

        public GoogleAIService(IGoogleAIClient googleAIClient)
        {
            this.googleAIClient = googleAIClient;
        }
        public async Task<string> GeminiRecommendations(WeatherPredictionResult conditions)
        {
            string message = $@"
                            You are a helpful weather advisor. Based on the data below:

                            1. Provide a short, friendly activity recommendation.
                            2. Add a 'Planning Tip:' with advice on what to wear or take.
                            3. If weather is dangerous (extreme heat, storms, high winds), add 'Danger Alert:'.

                            Weather Data:
                            - avgTemp: {conditions.AvgTemp}
                            - avgPrecipitation: {conditions.AvgPrecipitation}
                            - avgHumidity: {conditions.AvgHumidity}
                            - avgWindSpeed: {conditions.AvgWindSpeed}
                            - hotTempPercent: {conditions.HotTempPercent}
                            - coldTempPercent: {conditions.ColdTempPercent}
                            - precipitationPercent: {conditions.PrecipitationPercent}
                            - highHumidityPercent: {conditions.HighHumidityPercent}
                            - highWindSpeedPercent: {conditions.HighWindSpeedPercent}
                            - solarRadiationPercent: {conditions.SolarRadiationPercent}";

            return  await googleAIClient.AskGemini(message);
        }

    }
}
