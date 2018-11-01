namespace DarkSky.Ui.Desktop.Components.Main
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Application.Domain.Services;

    public class MainWindowViewModel : ComponentViewModel
    {
        private readonly IForecastService forecastService;
        private readonly ILocationService locationService;
        private IObservable<Forecast> forecastObservable;

        public MainWindowViewModel(IForecastService forecastService, ILocationService locationService)
        {
            this.forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
            this.locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }

        private List<Location> locationList;
        private Location selectedLocation;

        public List<Location> LocationList
        {
            get => this.locationList;
            set
            {
                this.Set(() => this.LocationList, ref this.locationList, value);
            }
        }

        public Location SelectedLocation
        {
            get => this.selectedLocation;
            set
            {
                this.Set(() => this.SelectedLocation, ref this.selectedLocation, value);
            }
        }

        public override void Init()
        {
            // load locations
            this.locationService.GetFavoriteLocationsAsync(this.CancellationToken)
                .ToObservable().Subscribe(list => this.LocationList = list);

            this.forecastObservable = this.forecastService.GetForecastAsync(this.SelectedLocation, this.CancellationToken)
               .ToObservable();

            base.Init();
        }
    }
}