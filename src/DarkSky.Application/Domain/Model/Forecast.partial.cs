namespace DarkSky.Application.Domain.Model
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Calculated values
    /// </summary>
    public partial class Forecast
    {
        public string Day => string.Format(CultureInfo.CurrentUICulture, "{0:ddd}", this.Time.GetValueOrDefault());

        public string FeelsAsString => string.Format(CultureInfo.CurrentUICulture, "({0}° - {1}°)", Math.Round(this.ApparentTemperatureLow ?? 0), Math.Round(this.ApparentTemperatureHigh ?? 0));

        public double HumidityPercentage => (this.Humidity ?? 0) * 100;

        public double PressureRounded => Math.Round(this.Pressure ?? 0);

        public string TemperatureAsString => string.Format(CultureInfo.CurrentUICulture, "{0}° - {1}°", Math.Round(this.TemperatureLow ?? 0), Math.Round(this.TemperatureHigh ?? 0));

        public double WindSpeedRounded => Math.Round(this.WindSpeed ?? 0);
    }
}