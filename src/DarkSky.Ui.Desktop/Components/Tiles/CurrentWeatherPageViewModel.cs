namespace DarkSky.Ui.Desktop.Components.Tiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Application.Domain.Services;
    using DarkSky.Ui.Desktop.Navigation;
    using GalaSoft.MvvmLight.Views;

    public class CurrentWeatherPageViewModel : ComponentViewModel
    {
        private readonly IForecastService forecastService;
        private readonly INavigationService navigationService;
        private CurrentWeather currentForecast;
        private List<Forecast> dailyForecast;
        private bool isBusy;
        private bool isReady;

        public CurrentWeatherPageViewModel(IForecastService forecastService, INavigationService navigationService)
        {
            this.forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
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

        public override void Init()
        {
            if (PageNavigationService.ExtraData is Location selectedLocation)
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