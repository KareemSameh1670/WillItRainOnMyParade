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
        public Task<WeatherPredictionResult> GetDailyProbabilities(float lat, float lon, DateTime date, int NumOfYears = 10);
    }
}
