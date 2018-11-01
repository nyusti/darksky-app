namespace DarkSky.Application.Domain.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Client;

    public class DarkSkyForecastService : IForecastService
    {
        private readonly IDarkSkyClient darkSkyClient;

        public DarkSkyForecastService(IDarkSkyClient darkSkyClient)
        {
            this.darkSkyClient = darkSkyClient ?? throw new ArgumentNullException(nameof(darkSkyClient));
        }

        public Task<Forecast> GetForecastAsync(Location location, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}