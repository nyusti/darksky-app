namespace DarkSky.Ui.Desktop.Components.Main
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Threading;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Application.Domain.Services;

    public class MainWindowViewModel : ComponentViewModel
    {
        private readonly IForecastService forecastService;
        private readonly ILocationService locationService;

        public MainWindowViewModel(IForecastService forecastService, ILocationService locationService)
        {
            this.forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
            this.locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }

        private List<Location> locationList;
        private Location selectedLocation;
        private Forecast currentForecast;

        private bool isForecastLoading;
        private CancellationTokenSource localTokenSource;

        public List<Location> LocationList
        {
            get => this.locationList;
            set => this.Set(() => this.LocationList, ref this.locationList, value);
        }

        public Location SelectedLocation
        {
            get => this.selectedLocation;
            set => this.Set(() => this.SelectedLocation, ref this.selectedLocation, value);
        }

        public bool IsForecastLoading
        {
            get => this.isForecastLoading;
            set => this.Set(() => this.IsForecastLoading, ref this.isForecastLoading, value);
        }

        public Forecast CurrentForecast
        {
            get => this.currentForecast;
            set => this.Set(() => this.CurrentForecast, ref this.currentForecast, value);
        }

        public override void Init()
        {
            // load locations
            this.locationService.GetFavoriteLocationsAsync(this.CancellationToken)
                .ToObservable()
                .Subscribe(list => this.LocationList = list, this.CancellationToken);

            this.localTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationToken);

            this.OnPropertyChanges(p => p.SelectedLocation)
                .Subscribe(location => this.LoadForecast(location));

            base.Init();
        }

        private void LoadForecast(Location location)
        {
            this.CancelIfInProgress();
            this.IsForecastLoading = true;
            this.forecastService.GetForecastAsync(location, this.localTokenSource.Token)
                .ToObservable()
                .Finally(() => this.IsForecastLoading = false)
                .Subscribe(forecast => this.CurrentForecast = forecast, this.localTokenSource.Token);
        }

        private void CancelIfInProgress()
        {
            if (!this.localTokenSource.IsCancellationRequested)
            {
                this.localTokenSource.Cancel();
                this.localTokenSource.Dispose();
                this.localTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationTokenSource.Token);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}