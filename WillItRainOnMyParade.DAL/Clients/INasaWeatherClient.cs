using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillItRainOnMyParade.DAL.DTOs;

namespace WillItRainOnMyParade.DAL.Clients
{
    public interface INasaWeatherClient
    {
        public Task<List<WeatherConditions>> GetDailyDataAsync(float lat, float lon, DateTime start, DateTime end);
    }
}
