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
        Task<WeatherPredictionResult> GetDailyMean(double lat, double lon, DateTime date);
    }
}
