namespace DarkSky.Ui.Desktop.Components.Main
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Threading;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Application.Domain.Services;
    using DarkSky.Application.Extensions;
    using GalaSoft.MvvmLight.CommandWpf;

    /// <summary>
    /// The view model for the shell
    /// </summary>
    /// <seealso cref="DarkSky.Ui.Desktop.Components.ComponentViewModel"/>
    public class MainWindowViewModel : ComponentViewModel
    {
        private readonly IForecastService forecastService;
        private readonly ILocationService locationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="forecastService">The forecast service.</param>
        /// <param name="locationService">The location service.</param>
        /// <exception cref="ArgumentNullException">forecastService or locationService is null</exception>
        public MainWindowViewModel(IForecastService forecastService, ILocationService locationService)
        {
            this.forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
            this.locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }

        private List<Location> locationList;
        private Location selectedLocation;
        private Forecast currentForecast;
        private RelayCommand<string> openLinkCommand;

        private bool isBusy;
        private CancellationTokenSource internalTokenSource;
        private IDisposable propertyChangedSubscriber;

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

        public bool IsBusy
        {
            get => this.isBusy;
            set => this.Set(() => this.IsBusy, ref this.isBusy, value);
        }

        public Forecast CurrentForecast
        {
            get => this.currentForecast;
            set => this.Set(() => this.CurrentForecast, ref this.currentForecast, value);
        }

        public RelayCommand<string> OpenLinkCommand => this.openLinkCommand ?? (this.openLinkCommand = new RelayCommand<string>(url => Process.Start(url)));

        /// <inheritdoc/>
        public override void Init()
        {
            // load locations
            this.IsBusy = true;
            this.locationService.GetFavoriteLocationsAsync(this.CancellationToken)
                .ToObservable()
                .Finally(() => this.IsBusy = false)
                .Subscribe(locations => this.LocationList = locations, this.CancellationToken);

            // create an internal cancellation token source to support service cal cancellation
            this.internalTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationToken);

            // subscribe to location change
            this.propertyChangedSubscriber = this.OnPropertyChanges(p => p.SelectedLocation)
                .Subscribe(location => this.LoadForecast(location));

            base.Init();
        }

        /// <summary>
        /// gets the forecast based on the location
        /// </summary>
        /// <param name="location">The location.</param>
        /// <exception cref="ArgumentNullException">location is null</exception>
        private void LoadForecast(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            this.CancelIfInProgress();
            this.IsBusy = true;
            this.forecastService.GetForecastAsync(location, ApplicationContext.UserContext.SelectedLanguage, this.internalTokenSource.Token)
                .ToObservable()
                .Finally(() => this.IsBusy = false)
                .Subscribe(forecast => this.CurrentForecast = forecast, this.internalTokenSource.Token);
        }

        /// <summary>
        /// Cancels if in progress.
        /// </summary>
        private void CancelIfInProgress()
        {
            if (!this.internalTokenSource.IsCancellationRequested)
            {
                this.internalTokenSource.Cancel();
                this.internalTokenSource.Dispose();
                this.internalTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationTokenSource.Token);
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            this.propertyChangedSubscriber.Dispose();
            base.Dispose(disposing);
        }
    }
}