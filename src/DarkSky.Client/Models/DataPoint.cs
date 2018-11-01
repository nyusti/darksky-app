namespace DarkSky.Client.Models
{
    using System;
    using Newtonsoft.Json;

    public partial class DataPoint
    {
        [JsonProperty(PropertyName = "apparentTemperature")]
        public double? ApparentTemperature { get; set; }

        [JsonProperty(PropertyName = "temperature")]
        public double? Temperature { get; set; }

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        [JsonProperty(PropertyName = "apparentTemperatureMin")]
        public double? ApparentTemperatureMin { get; set; }

        [JsonProperty(PropertyName = "apparentTemperatureMax")]
        public double? ApparentTemperatureMax { get; set; }

        [JsonProperty(PropertyName = "temperatureMin")]
        public double? TemperatureMin { get; set; }

        [JsonProperty(PropertyName = "temperatureMax")]
        public double? TemperatureMax { get; set; }

        [JsonProperty(PropertyName = "pressure")]
        public double? Pressure { get; set; }

        [JsonProperty(PropertyName = "windSpeed")]
        public double? WindSpeed { get; set; }

        [JsonProperty(PropertyName = "humidity")]
        public double? Humidity { get; set; }

        [JsonProperty(PropertyName = "uvIndex")]
        public double? UvIndex { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "time")]
        public DateTimeOffset? Time { get; set; }
    }
}