namespace DarkSky.Application.Domain.Model
{
    using System;

    /// <summary>
    /// Forecast model
    /// </summary>
    public partial class Forecast
    {
        public double? ApparentTemperatureHigh { get; set; }

        public double? ApparentTemperatureLow { get; set; }

        public double? Humidity { get; set; }

        public string Icon { get; set; }

        public double? Pressure { get; set; }

        public string Summary { get; set; }

        public double? TemperatureHigh { get; set; }

        public double? TemperatureLow { get; set; }

        public DateTimeOffset? Time { get; set; }

        public double? UvIndex { get; set; }

        public double? WindSpeed { get; set; }
    }
}