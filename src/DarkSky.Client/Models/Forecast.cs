namespace DarkSky.Client.Models
{
    using Newtonsoft.Json;

    public partial class Forecast
    {
        [JsonProperty(PropertyName = "currently")]
        public DataPoint Currently { get; set; }

        [JsonProperty(PropertyName = "daily")]
        public DataBlock Daily { get; set; }
    }
}