using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WillItRainOnMyParade.DAL.DTOs;

namespace WillItRainOnMyParade.DAL.Clients
{
    public class NasaWeatherClient : INasaWeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration config;

        public NasaWeatherClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            this.config = config;
        }

        public async Task<List<WeatherConditions>> GetDailyDataAsync(float lat, float lon, DateTime start, DateTime end)
        {
            string url = config["NasaPowerAPI:URL"] +
             $"?start={start:yyyyMMdd}&end={end:yyyyMMdd}" +
             $"&latitude={lat}&longitude={lon}" +
             $"&parameters=T2M,PRECTOTCORR,RH2M,WS2M,ALLSKY_KT" +
             $"&community=ag&format=JSON";


            using var stream = await _httpClient.GetStreamAsync(url);
            var CondationsList = new List<WeatherConditions>();

            await foreach (var wc in WeatherDataParser.ReadWeatherAsync(stream, d => d.Month == start.Month && d.Day == start.Day, start, end))
            {
                CondationsList.Add(wc);
            }
            return CondationsList;
        }

    }
}
