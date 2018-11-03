namespace DarkSky.Ui.Desktop.Components.Main
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Application.Domain.Services;
    using DarkSky.Application.Extensions;
    using DarkSky.Ui.Desktop.Localization;
    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Views;

    /// <summary>
    /// The view model for the shell
    /// </summary>
    /// <seealso cref="DarkSky.Ui.Desktop.Components.ComponentViewModel"/>
    public class MainWindowViewModel : ComponentViewModel
    {
        private readonly ILanguageService languageService;
        private readonly ILocationService locationService;
        private readonly INavigationService navigationService;
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
        public MainWindowViewModel(ILocationService locationService, ILanguageService languageService, INavigationService navigationService)
        {
            this.locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            this.languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public List<Language> Languages
        {
            get => this.languages;
            set => this.Set(() => this.Languages, ref this.languages, value);
        }

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
            // subscribe to location change
            this.propertyChangedSubscriber = this.OnPropertyChanges(p => p.SelectedLocation)
                .Subscribe(location => this.navigationService.NavigateTo("/Components/Tiles/CurrentWeatherPage.xaml", this.SelectedLocation));

            this.OnPropertyChanges(p => p.SelectedLanguage)
                .Where(language => language != null)
                .Subscribe(language =>
                {
                    this.languageService.ChangeLanguage(language);
                    if (this.SelectedLocation != null)
                    {
                        this.navigationService.NavigateTo("/Components/Tiles/CurrentWeatherPage.xaml", this.SelectedLocation);
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

            this.locationService.GetFavoriteLocationsAsync(this.CancellationToken)
                .ToObservable()
                .Subscribe(locations =>
                {
                    this.LocationList = locations;
                }, this.CancellationToken);

            this.navigationService.NavigateTo("/Components/Welcome/WelcomePage.xaml");

            base.Init();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            this.propertyChangedSubscriber.Dispose();
            base.Dispose(disposing);
        }
    }
}