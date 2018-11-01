namespace DarkSky.Client
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Client.Models;
    using Microsoft.Rest;

    /// <summary>
    /// Forecast HTTP operation interface
    /// </summary>
    public partial interface IForecastOperations
    {
        /// <summary>
        /// Gets the forecast with HTTP messages asynchronous.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="lang">The language.</param>
        /// <param name="customHeaders">The custom headers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response message.</returns>
        Task<HttpOperationResponse<Forecast>> GetForecastWithHttpMessagesAsync(double latitude, double longitude, string lang = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}