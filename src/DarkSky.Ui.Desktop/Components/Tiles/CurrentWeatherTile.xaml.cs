namespace DarkSky.Ui.Desktop.Components.Tiles
{
    using System.Windows;
    using System.Windows.Controls;
    using DarkSky.Application.Domain.Model;

    /// <summary>
    /// Interaction logic for CurrentWeatherTitle.xaml
    /// </summary>
    public partial class CurrentWeatherTile : UserControl
    {
        public static readonly DependencyProperty ForecastProperty =
            DependencyProperty.Register("Forecast", typeof(CurrentWeather), typeof(CurrentWeatherTile), new PropertyMetadata(ForecastCallback));

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentWeatherTile"/> class.
        /// </summary>
        public CurrentWeatherTile()
        {
            this.InitializeComponent();
            this.ViewModel = new CurrentWeatherTileViewModel();
        }

        public CurrentWeather Forecast
        {
            get { return (CurrentWeather)this.GetValue(ForecastProperty); }
            set { this.SetValue(ForecastProperty, value); }
        }

        public CurrentWeatherTileViewModel ViewModel
        {
            get => this.LayoutGrid.DataContext as CurrentWeatherTileViewModel;
            set => this.LayoutGrid.DataContext = value;
        }

        private static void ForecastCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CurrentWeatherTile tile && e.NewValue != e.OldValue && e.NewValue is CurrentWeather details)
            {
                var viewModel = tile.ViewModel;
                viewModel.IconName = details.Icon;
                viewModel.Temperature = $"{details.TemperatureAsString} {details.Summary}";
                viewModel.FeelsLike = details.FeelsAsString;
                viewModel.Summary = details.DailySummary;
                viewModel.Wind = $"{details.WindSpeedRounded} kph";
                viewModel.Humidity = $"{details.HumidityPercentage}%";
                viewModel.UvIndex = $"{details.UvIndex}";
                viewModel.Pressure = $"{details.PressureRounded} hPa";
            }
        }
    }
}