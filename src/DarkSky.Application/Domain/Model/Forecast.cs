using System.Collections.Generic;

namespace DarkSky.Application.Domain.Model
{
    public partial class Forecast
    {
        public ForecastDetails CurrentForecast { get; set; }

        public string DailyForecastSummary { get; set; }

        public List<ForecastDetails> DailyForecasts { get; set; }
    }
}