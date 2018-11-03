namespace DarkSky.Ui.Desktop.Components.Main
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Threading;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Application.Domain.Services;
    using DarkSky.Application.Extensions;
    using DarkSky.Ui.Desktop.Localization;
    using GalaSoft.MvvmLight.CommandWpf;

    /// <summary>
    /// The view model for the shell
    /// </summary>
    /// <seealso cref="DarkSky.Ui.Desktop.Components.ComponentViewModel"/>
    public class MainWindowViewModel : ComponentViewModel
    {
        private readonly IForecastService forecastService;
        private readonly ILanguageService languageService;
        private readonly ILocationService locationService;

        private CurrentWeather currentForecast;
        private List<Forecast> dailyForecast;
        private CancellationTokenSource internalTokenSource;
        private bool isBusy;
        private List<Language> languages;
        private List<Location> locationList;
        private RelayCommand<string> openLinkCommand;
        private IDisposable propertyChangedSubscriber;
        private Language selectedLanguage;
        private Location selectedLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="forecastService">The forecast service.</param>
        /// <param name="locationService">The location service.</param>
        /// <exception cref="ArgumentNullException">forecastService or locationService is null</exception>
        public MainWindowViewModel(IForecastService forecastService, ILocationService locationService, ILanguageService languageService)
        {
            this.forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
            this.locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            this.languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
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

        public List<Language> Languages
        {
            get => this.languages;
            set => this.Set(() => this.Languages, ref this.languages, value);
        }

        public ILanguageService Loc => this.languageService;

        public List<Location> LocationList
        {
            get => this.locationList;
            set => this.Set(() => this.LocationList, ref this.locationList, value);
        }

        public RelayCommand<string> OpenLinkCommand => this.openLinkCommand ?? (this.openLinkCommand = new RelayCommand<string>(url => Process.Start(url)));

        public Language SelectedLanguage
        {
            get => this.selectedLanguage;
            set => this.Set(() => this.SelectedLanguage, ref this.selectedLanguage, value);
        }

        public Location SelectedLocation
        {
            get => this.selectedLocation;
            set => this.Set(() => this.SelectedLocation, ref this.selectedLocation, value);
        }

        /// <inheritdoc/>
        public override void Init()
        {
            // create an internal cancellation token source to support service cal cancellation
            this.internalTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationToken);

            // subscribe to location change
            this.propertyChangedSubscriber = this.OnPropertyChanges(p => p.SelectedLocation)
                .Subscribe(location => this.LoadForecast(location));

            this.OnPropertyChanges(p => p.SelectedLanguage)
                .Where(language => language != null)
                .Subscribe(language =>
                {
                    this.languageService.ChangeLanguage(language);
                    if (this.SelectedLocation != null)
                    {
                        this.LoadForecast(this.SelectedLocation);
                    }
                });

            // load languages
            this.languageService.GetSupportedLanguagesAsync(this.CancellationToken)
                .ToObservable()
                .Subscribe(languages =>
                {
                    this.Languages = languages;
                    this.SelectedLanguage = this.Languages.FirstOrDefault();
                }, this.CancellationToken);

            // load locations
            this.IsBusy = true;
            this.locationService.GetFavoriteLocationsAsync(this.CancellationToken)
                .ToObservable()
                .Finally(() => this.IsBusy = false)
                .Subscribe(locations =>
                {
                    this.LocationList = locations;
                    this.SelectedLocation = this.LocationList.FirstOrDefault();
                }, this.CancellationToken);

            base.Init();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            this.propertyChangedSubscriber.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Cancels if in progress.
        /// </summary>
        private void CancelIfInProgress()
        {
            if (this.internalTokenSource?.IsCancellationRequested == true)
            {
                this.internalTokenSource.Cancel();
                this.internalTokenSource.Dispose();
                this.internalTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationTokenSource.Token);
            }
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
                .Subscribe(forecast =>
                {
                    this.CurrentForecast = forecast;
                    this.DailyForecast = forecast.Daily.OrderBy(p => p.Time).ToList();
                }, this.internalTokenSource.Token);
        }
    }
}