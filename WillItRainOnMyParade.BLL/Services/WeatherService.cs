using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WillItRainOnMyParade.BLL.DTOs;
using WillItRainOnMyParade.BLL.Interfaces;
using WillItRainOnMyParade.DAL.Clients;
using WillItRainOnMyParade.DAL.DTOs;

namespace WillItRainOnMyParade.BLL.Services
{
   
    
        public class WeatherService : IWeatherService
        {
        private readonly INasaWeatherClient nasaWeatherClient;
        // Temperature (°C)
        public const float VeryHot = 38.0f;
        public const float VeryCold = 8.0f;

        // Wind speed (m/s)
        public const float VeryWindy = 12.0f;

        // Precipitation (mm/day)
        public const float VeryWet = 15.0f;

        // Humidity (%)
        public const float VeryHumid = 70.0f;

        public WeatherService(INasaWeatherClient nasaWeatherClient)=>this.nasaWeatherClient = nasaWeatherClient;

            public async Task<WeatherPredictionResult> GetDailyProbabilities(float lat, float lon,DateTime date, int NumOfYears=10)
            {
                DateTime endDate = new DateTime (DateTime.Now.AddYears(-1).Year, 12, 31);
            int MaxYearsRange = endDate.Year - new DateTime(1980, 1, 1).Year;
            if (NumOfYears > MaxYearsRange)
                throw new ArgumentException($"Invalid Date Range, Please make sure you are not Exceeding {MaxYearsRange} Years");
                DateTime startDate = new (endDate.AddYears(-NumOfYears).Year, date.Month, date.Day);
                

                var weatherRecords= await nasaWeatherClient.GetDailyDataAsync(lat, lon, startDate, endDate);
                
                if (weatherRecords == null) 
                    weatherRecords= new List<WeatherConditions>();
                return Search(weatherRecords, startDate, endDate);
            }
        private WeatherPredictionResult Search(List<WeatherConditions> weatherRecords, DateTime startDate, DateTime endDate)
        {
            float NumOfHotDays = 0, NumOfColdDays = 0, NumOfWindyDays = 0, NumOfWetDays = 0, NumOfHumidDays = 0;
            int CurrYear = startDate.Year;
            int count=0;
            while(startDate < endDate)
            {
            var weatherConditions = weatherRecords[startDate.DayOfYear - 1];

                if (weatherConditions.T2M > VeryHot)    NumOfHotDays++;
                else if (weatherConditions.T2M < VeryCold)  NumOfColdDays++;

                if (weatherConditions.WS2M > VeryWindy) NumOfWindyDays++;

                if(weatherConditions.RH2M > VeryHumid)  NumOfHumidDays++;

                if(weatherConditions.PRECTOTCORR > VeryWet) NumOfWetDays++;

                startDate = startDate.AddYears(1);
                count++;
            }
            var Result = new WeatherPredictionResult 
            {
                HotTempPercent = NumOfHotDays/count, ColdTempPercent = NumOfColdDays/count,
                HumidityPercent = NumOfHumidDays/count, PrecipitationPercent= NumOfWetDays/count,
                WindSpeedPercent= NumOfWindyDays/count 
            };
            return Result;
        }
    }
}

