namespace DarkSky.Application.Domain.Model
{
    using System;

    /// <summary>
    /// Calculated values
    /// </summary>
    public partial class Forecast
    {
        public string TemperatureAsString => string.Format("{0}° - {1}°", Math.Round(this.TemperatureLow ?? 0), Math.Round(this.TemperatureHigh ?? 0));

        public string Feels => string.Format("{0}° - {1}°", Math.Round(this.ApparentTemperatureLow ?? 0), Math.Round(this.ApparentTemperatureHigh ?? 0));

        public double WindSpeedRounded => Math.Round(this.WindSpeed ?? 0);

        public double HumidityPercentage => (this.Humidity ?? 0) * 100;

        public double PressureRounded => Math.Round(this.Pressure ?? 0);
    }
}