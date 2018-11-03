namespace DarkSky.Application.Domain.Model
{
    using System;
    using System.Collections.Generic;

    public partial class CurrentWeather
    {
        public double? ApparentTemperature { get; set; }

        public List<Forecast> Daily { get; set; }

        public string DailySummary { get; set; }

        public double? Humidity { get; set; }

        public string Icon { get; set; }

        public double? Pressure { get; set; }

        public string Summary { get; set; }

        public double? Temperature { get; set; }

        public DateTimeOffset? Time { get; set; }

        public double? UvIndex { get; set; }

        public double? WindSpeed { get; set; }
    }
}