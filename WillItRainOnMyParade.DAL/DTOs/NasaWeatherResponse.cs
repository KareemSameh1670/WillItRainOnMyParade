using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillItRainOnMyParade.DAL.DTOs
{
    public class NasaWeatherResponse
    {
        public Properties Properties { get; set; }
    }

    public class Properties
    {
        public Parameter Parameters { get; set; }
    }

    public class Parameter
    {
        public Dictionary<string, double> T2M { get; set; }
        public Dictionary<string, double> PRECTOTCORR { get; set; }
        public Dictionary<string, double> RH2M { get; set; }
        public Dictionary<string, double> WS2M { get; set; }
        public Dictionary<string, double> ALLSKY_KT { get; set; }
    }
}
