namespace DarkSky.Application.Domain.Model
{
    using System.Collections.Generic;

    public partial class Forecast
    {
        public ForecastDetails Current { get; set; }

        public string DailySummary { get; set; }

        public List<ForecastDetails> Daily { get; set; }
    }
}