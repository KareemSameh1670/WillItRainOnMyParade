using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillItRainOnMyParade.BLL.DTOs;
using WillItRainOnMyParade.BLL.Interfaces;
using WillItRainOnMyParade.DAL.Clients;
using WillItRainOnMyParade.DAL.DTOs;

namespace WillItRainOnMyParade.BLL.Services
{
   
    
        public class WeatherService : IWeatherService
        {
            private readonly HttpClient _httpClient;

            public WeatherService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<WeatherPredictionResult> GetDailyMean(double lat, double lon, DateTime date)
            {
                // NASA POWER requires YYYYMMDD format
                string start = date.AddYears(-10).ToString("yyyyMMdd");
                string end = date.ToString("yyyyMMdd");
                string dateKey = date.ToString("yyyyMMdd");

                // Build request URL
                string url = $"https://power.larc.nasa.gov/api/temporal/daily/point?" +
                             $"start={start}&end={end}&latitude={lat}&longitude={lon}" +
                             $"&community=ag&parameters=T2M,PRECTOT,RH2M,WS2M,ALLSKY_SFC_SW_DWN" +
                             $"&format=json";

                // Fetch from NASA
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"NASA API error: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var root = JObject.Parse(json);

                var parameters = root["properties"]?["parameter"];
                if (parameters == null)
                    throw new Exception("NASA API response did not contain expected data");

                // Extract values for given day
                double temperature = parameters["T2M"]?[dateKey]?.Value<double>() ?? 0;
                double precipitation = parameters["PRECTOT"]?[dateKey]?.Value<double>() ?? 0;
                double humidity = parameters["RH2M"]?[dateKey]?.Value<double>() ?? 0;
                double windSpeed = parameters["WS2M"]?[dateKey]?.Value<double>() ?? 0;
                double solarRadiation = parameters["ALLSKY_SFC_SW_DWN"]?[dateKey]?.Value<double>() ?? 0;

                return new WeatherPredictionResult
                {
                    Temperature = temperature,
                    Precipitation = precipitation,
                    Humidity = humidity,
                    WindSpeed = windSpeed,
                    SolarRadiation = solarRadiation
                };
            }
        }
}

