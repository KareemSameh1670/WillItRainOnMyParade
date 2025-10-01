using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillItRainOnMyParade.BLL.DTOs;

namespace WillItRainOnMyParade.BLL.Interfaces
{
    public interface IWeatherService
    {
        // Temperature (°C)
        public const float VeryHot = 38.0f;
        public const float VeryCold = 8.0f;

        // Wind speed (m/s)
        public const float VeryWindy = 12.0f;

        // Precipitation (mm/day)
        public const float VeryWet = 15.0f;

        // Humidity (%)
        public const float VeryHumid = 70.0f;
        public Task<WeatherPredictionResult> GetDailyProbabilities(float lat, float lon, DateTime date, int NumOfYears = 10);
    }
}
