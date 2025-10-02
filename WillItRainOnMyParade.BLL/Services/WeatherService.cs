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
        private readonly IGoogleAIService googleAIService;

        #region Standard Conditions
        // Temperature (°C)
        public const float VeryHot = 30.0f;
        public const float VeryCold = 15.0f;

        // Wind speed (m/s)
        public const float VeryWindy = 10.0f;

        // Precipitation (mm/day)
        public const float VeryWet = 0.0f;

        // Humidity (%)
        public const float VeryHumid = 70.0f;
        #endregion        
        public WeatherService(INasaWeatherClient nasaWeatherClient, IGoogleAIService googleAIService)
        {
            this.nasaWeatherClient = nasaWeatherClient;
            this.googleAIService = googleAIService;
        }

        public async Task<WeatherPredictionResult> GetDailyProbabilities(float lat, float lon,DateTime date, int NumOfYears=10)
        {
            DateTime endDate = new DateTime (DateTime.Now.AddYears(-1).Year, 12, 31);
            int MaxYearsRange = endDate.Year - new DateTime(1980, 1, 1).Year-1;
            if (NumOfYears > MaxYearsRange)
                throw new ArgumentException($"Invalid Date Range, Please make sure you are not Exceeding {MaxYearsRange} Years");
            DateTime startDate = new (endDate.AddYears(-NumOfYears).Year, date.Month, date.Day);
                

            var weatherRecords= await nasaWeatherClient.GetDailyDataAsync(lat, lon, startDate, endDate);
                
            if (weatherRecords == null) 
                weatherRecords= new List<WeatherConditions>();
            var Result= CalculateProbabilities(weatherRecords);

            return Result;
        }
        private WeatherPredictionResult CalculateProbabilities(List<WeatherConditions> weatherRecords)
        {
            float NumOfHotDays = 0, NumOfColdDays = 0, NumOfWindyDays = 0, NumOfWetDays = 0, NumOfHumidDays = 0;
            float TotalTemp = 0, TotalHumidty = 0, TotalPrecipitation = 0, TotalWindSpeed = 0;
            int count= weatherRecords.Count;
            foreach (var weatherRecord in weatherRecords)
            {
                if (weatherRecord.T2M > VeryHot)    NumOfHotDays++;
                else if (weatherRecord.T2M < VeryCold)  NumOfColdDays++;

                if (weatherRecord.WS2M > VeryWindy) NumOfWindyDays++;

                if(weatherRecord.RH2M > VeryHumid)  NumOfHumidDays++;

                if(weatherRecord.PRECTOTCORR > VeryWet) NumOfWetDays++;

                TotalTemp += weatherRecord.T2M;
                TotalHumidty += weatherRecord.RH2M;
                TotalPrecipitation += weatherRecord.PRECTOTCORR;
                TotalWindSpeed += weatherRecord.WS2M;

            }
            var Result = new WeatherPredictionResult 
            {
                AvgTemp = TotalTemp/count, AvgHumidity= TotalHumidty/count,
                AvgPrecipitation = TotalPrecipitation/count, AvgWindSpeed= TotalWindSpeed/count,
                HotTempPercent = NumOfHotDays/count*100, ColdTempPercent = NumOfColdDays/count*100,
                HighHumidityPercent = NumOfHumidDays/count * 100, PrecipitationPercent= NumOfWetDays/count * 100,
                HighWindSpeedPercent = NumOfWindyDays/count * 100
            };
            return Result;
        }


    }
}

