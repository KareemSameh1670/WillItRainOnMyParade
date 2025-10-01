using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillItRainOnMyParade.DAL.DTOs
{

    public class WeatherConditions
    {
        public float T2M { get; set; }
        public float PRECTOTCORR { get; set; }
        public float RH2M { get; set; }
        public float WS2M { get; set; }
        public float ALLSKY_KT { get; set; }
        public DateTime Date { get; set; }
    }
}
