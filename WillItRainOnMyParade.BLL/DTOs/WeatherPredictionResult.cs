using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillItRainOnMyParade.BLL.DTOs
{
    public class WeatherPredictionResult
    {
        public double Temperature { get; set; }
        public double Precipitation { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double SolarRadiation { get; set; }

        // You can also add a simple interpretation for tourism
        public string SkyCondition
        {
            get
            {
                if (Precipitation > 0.5) return "Rainy";
                if (SolarRadiation > 0.6) return "Sunny";
                return "Cloudy";
            }
        }
    }
}
