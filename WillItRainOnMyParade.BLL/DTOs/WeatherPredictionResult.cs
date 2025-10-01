using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillItRainOnMyParade.BLL.DTOs
{
    public class WeatherPredictionResult
    {
        public float HotTempPercent { get; set; }
        public float ColdTempPercent { get; set; }
        public float PrecipitationPercent { get; set; }
        public float HumidityPercent { get; set; }
        public float WindSpeedPercent { get; set; }
        public float SolarRadiationPercent { get; set; }
    }
}
