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

        public NasaWeatherClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WeatherConditions>> GetDailyDataAsync(float lat, float lon, DateTime start, DateTime end)
        {
            string url = $"https://power.larc.nasa.gov/api/temporal/daily/point" +
             $"?start={start:yyyyMMdd}&end={end:yyyyMMdd}" +
             $"&latitude={lat}&longitude={lon}" +
             $"&parameters=T2M,PRECTOTCORR,RH2M,WS2M,ALLSKY_KT" +
             $"&community=ag&format=JSON";

            var response = await _httpClient.GetAsync(url);
            

            if (!response.IsSuccessStatusCode) throw new NullReferenceException("Bad Request"); ;

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<WeatherConditions>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Console.WriteLine("NASA RESPONSE:");
            Console.WriteLine(json);
            if (data == null) throw new NullReferenceException("Invalid URL");
            return data;
        }

    }
}
