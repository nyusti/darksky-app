namespace DarkSky.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Client.Models;

    /// <summary>
    /// Forecast operations extensions
    /// </summary>
    public static class ForecastOperationsExtensions
    {
        /// <summary>
        /// Gets the forecast asynchronous.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="lang">The language.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The forecast</returns>
        public static async Task<Forecast> GetForecastAsync(this IForecastOperations operations, double latitude, double longitude, string lang = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var result = await operations.GetForecastWithHttpMessagesAsync(latitude, longitude, lang, null, cancellationToken).ConfigureAwait(false))
            {
                return result.Body;
            }
        }
    }
}