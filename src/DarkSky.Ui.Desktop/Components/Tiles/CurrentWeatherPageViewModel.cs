namespace DarkSky.Ui.Desktop.Components.Tiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Application.Domain.Services;

    /// <summary>
    /// Current wwather view model
    /// </summary>
    /// <seealso cref="DarkSky.Ui.Desktop.Components.ComponentViewModel"/>
    public class CurrentWeatherPageViewModel : ComponentViewModel
    {
        private readonly IForecastService forecastService;
        private CurrentWeather currentForecast;
        private List<Forecast> dailyForecast;
        private bool isBusy;
        private bool isReady;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentWeatherPageViewModel"/> class.
        /// </summary>
        /// <param name="forecastService">The forecast service.</param>
        /// <exception cref="ArgumentNullException">forecastService is null</exception>
        public CurrentWeatherPageViewModel(IForecastService forecastService)
        {
            this.forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
        }

        public CurrentWeather CurrentForecast
        {
            get => this.currentForecast;
            set => this.Set(() => this.CurrentForecast, ref this.currentForecast, value);
        }

        public List<Forecast> DailyForecast
        {
            get => this.dailyForecast;
            set => this.Set(() => this.DailyForecast, ref this.dailyForecast, value);
        }

        public bool IsBusy
        {
            get => this.isBusy;
            set => this.Set(() => this.IsBusy, ref this.isBusy, value);
        }

        public bool IsReady
        {
            get => this.isReady;
            set => this.Set(() => this.IsReady, ref this.isReady, value);
        }

        /// <inheritdoc/>
        public override void Init()
        {
            if (ApplicationContext.NavigationContext.NavigationState is Location selectedLocation
                && selectedLocation != null)
            {
                this.IsReady = false;
                this.IsBusy = true;
                this.forecastService.GetForecastAsync(selectedLocation, ApplicationContext.UserContext.SelectedLanguage, this.CancellationToken)
                    .ToObservable()
                    .Finally(() =>
                    {
                        this.IsBusy = false;
                        this.IsReady = true;
                    })
                    .Subscribe(forecast =>
                    {
                        this.CurrentForecast = forecast;
                        this.DailyForecast = forecast.Daily.OrderBy(p => p.Time).ToList();
                    }, this.CancellationToken);
            }

            base.Init();
        }
    }
}