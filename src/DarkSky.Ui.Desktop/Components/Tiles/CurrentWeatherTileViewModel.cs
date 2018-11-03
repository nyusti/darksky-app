namespace DarkSky.Ui.Desktop.Components.Tiles
{
    public class CurrentWeatherTileViewModel : ComponentViewModel
    {
        private string feelsLike;

        private string humidity;

        private string iconName;

        private string pressure;

        private string summary;

        private string temperature;

        private string uvIndex;

        private string wind;

        public string FeelsLike
        {
            get => this.feelsLike;
            set => this.Set(() => this.FeelsLike, ref this.feelsLike, value);
        }

        public string Humidity
        {
            get => this.humidity;
            set => this.Set(() => this.Humidity, ref this.humidity, value);
        }

        public string IconName
        {
            get => this.iconName;
            set => this.Set(() => this.IconName, ref this.iconName, value);
        }

        public string Pressure
        {
            get => this.pressure;
            set => this.Set(() => this.Pressure, ref this.pressure, value);
        }

        public string Summary
        {
            get => this.summary;
            set => this.Set(() => this.Summary, ref this.summary, value);
        }

        public string Temperature
        {
            get => this.temperature;
            set => this.Set(() => this.Temperature, ref this.temperature, value);
        }

        public string UvIndex
        {
            get => this.uvIndex;
            set => this.Set(() => this.UvIndex, ref this.uvIndex, value);
        }

        public string Wind
        {
            get => this.wind;
            set => this.Set(() => this.Wind, ref this.wind, value);
        }
    }
}