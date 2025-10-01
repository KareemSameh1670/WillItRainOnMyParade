using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillItRainOnMyParade.BLL.DTOs
{
    public class WeatherPredictionResult
    {
        public float AvgTemp{ get; set; }
        public float AvgPrecipitation{ get; set; }
        public float AvgHumidity{ get; set; }
        public float AvgWindSpeed { get; set; }

        public float HotTempPercent { get; set; }
        public float ColdTempPercent { get; set; }
        public float PrecipitationPercent { get; set; }
        public float HighHumidityPercent { get; set; }
        public float HighWindSpeedPercent { get; set; }

        public float SolarRadiationPercent { get; set; }
    }
}
