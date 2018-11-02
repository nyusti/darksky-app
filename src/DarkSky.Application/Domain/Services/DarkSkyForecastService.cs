namespace DarkSky.Application.Domain.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;
    using DarkSky.Client;

    /// <summary>
    /// Dark Sky service based forecast provider service
    /// </summary>
    /// <seealso cref="DarkSky.Application.Domain.Services.IForecastService"/>
    public class DarkSkyForecastService : IForecastService
    {
        private readonly IDarkSkyClient darkSkyClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkSkyForecastService"/> class.
        /// </summary>
        /// <param name="darkSkyClient">The dark sky client.</param>
        /// <exception cref="ArgumentNullException">darkSkyClient</exception>
        public DarkSkyForecastService(IDarkSkyClient darkSkyClient)
        {
            this.darkSkyClient = darkSkyClient ?? throw new ArgumentNullException(nameof(darkSkyClient));
        }

        /// <summary>
        /// Gets the forecast asynchronous.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="language">The language to query data for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The forecast for the location</returns>
        /// <exception cref="ArgumentNullException">location is null</exception>
        public virtual async Task<Forecast> GetForecastAsync(Location location, Languages language, CancellationToken cancellationToken)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            var result = await this.darkSkyClient.ForecastOperations.GetForecastAsync(location.Latitude, location.Longiture, language.ToString().ToLowerInvariant(), cancellationToken).ConfigureAwait(false);
            // TODO: map result

            return null;
        }
    }
}