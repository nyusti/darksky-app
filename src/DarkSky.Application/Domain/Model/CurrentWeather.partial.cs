namespace DarkSky.Application.Domain.Model
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Calculated values
    /// </summary>
    public partial class CurrentWeather
    {
        public string FeelsAsString => string.Format("{0}°", Math.Round(this.ApparentTemperature ?? 0), CultureInfo.CurrentUICulture);

        public double HumidityPercentage => (this.Humidity ?? 0) * 100;

        public double PressureRounded => Math.Round(this.Pressure ?? 0);

        public string TemperatureAsString => string.Format("{0}°", Math.Round(this.Temperature ?? 0), CultureInfo.CurrentUICulture);

        public double WindSpeedRounded => Math.Round(this.WindSpeed ?? 0);
    }
}