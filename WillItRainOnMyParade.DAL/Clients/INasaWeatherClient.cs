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
        Task<NasaWeatherResponse?> GetDailyDataAsync(double lat, double lon, DateTime start, DateTime end);
    }
}
