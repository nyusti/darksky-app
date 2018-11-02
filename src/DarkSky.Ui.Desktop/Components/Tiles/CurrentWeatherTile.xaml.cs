namespace DarkSky.Ui.Desktop.Components.Tiles
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using DarkSky.Application.Domain.Model;

    /// <summary>
    /// Interaction logic for CurrentWeatherTitle.xaml
    /// </summary>
    public partial class CurrentWeatherTile : UserControl
    {
        public static readonly DependencyProperty ForecastProperty =
            DependencyProperty.Register("Forecast", typeof(Forecast), typeof(CurrentWeatherTile), new PropertyMetadata(ForecastCallback));

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentWeatherTile"/> class.
        /// </summary>
        public CurrentWeatherTile()
        {
            this.InitializeComponent();
            this.ViewModel = new CurrentWeatherTileViewModel();
        }

        public Forecast Forecast
        {
            get { return (Forecast)this.GetValue(ForecastProperty); }
            set { this.SetValue(ForecastProperty, value); }
        }

        public CurrentWeatherTileViewModel ViewModel
        {
            get => this.LayoutGrid.DataContext as CurrentWeatherTileViewModel;
            set => this.LayoutGrid.DataContext = value;
        }

        private static void ForecastCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CurrentWeatherTile tile && e.NewValue != e.OldValue && e.NewValue is Forecast details)
            {
                var viewModel = tile.ViewModel;
                viewModel.IconName = details.Icon;
                viewModel.Temperature = $"{Math.Round(details.Temperature ?? 0)}° {details.Summary}";
                viewModel.FeelsLike = $"Feels like {Math.Round(details.ApparentTemperature ?? 0)}°";
                viewModel.Summary = details.DailySummary;
                viewModel.Wind = $"Wind: {Math.Round(details.WindSpeed ?? 0)} kph";
                viewModel.Humidity = $"Humidity: {details.Humidity * 100}%";
                viewModel.UvIndex = $"UV Index: {details.UvIndex}";
                viewModel.Pressure = $"Pressure: {Math.Round(details.Pressure ?? 0)} hPa";
            }
        }
    }
}