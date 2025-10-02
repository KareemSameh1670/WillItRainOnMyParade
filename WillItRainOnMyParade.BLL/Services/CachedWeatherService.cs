using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillItRainOnMyParade.BLL.DTOs;
using WillItRainOnMyParade.BLL.Interfaces;

namespace WillItRainOnMyParade.BLL.Services
{
    public class CachedWeatherService : IWeatherService
    {
        private readonly IWeatherService _inner;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _ttl;

        public CachedWeatherService(IWeatherService inner, IMemoryCache cache, TimeSpan? ttl = null)
        {
            _inner = inner;
            _cache = cache;
            _ttl = ttl ?? TimeSpan.FromMinutes(30);
        }

        public async Task<WeatherPredictionResult> GetDailyProbabilities(
            float lat, float lon, DateTime date, int NumOfYears = 10)
        {
            string key = $"{lat}_{lon}_{date:yyyyMMdd}_{NumOfYears}";

            if (_cache.TryGetValue(key, out WeatherPredictionResult cached))
            {
                Console.WriteLine($"[CACHE HIT] {key}");
                return cached;
            }

            Console.WriteLine($"[CACHE MISS] {key}");
            var result = await _inner.GetDailyProbabilities(lat, lon, date, NumOfYears);

            _cache.Set(key, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _ttl,   // TTL
                Priority = CacheItemPriority.Normal       // can also be High/Low
            });

            return result;
        }
    }
}
