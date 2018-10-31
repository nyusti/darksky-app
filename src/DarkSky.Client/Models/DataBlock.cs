namespace DarkSky.Client.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class DataBlock
    {
        [JsonProperty(PropertyName = "data")]
        public IList<DataPoint> Data { get; set; }

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }
    }
}