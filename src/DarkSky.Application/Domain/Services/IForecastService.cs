namespace DarkSky.Application.Domain.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;

    /// <summary>
    /// Forecast service interface
    /// </summary>
    public interface IForecastService
    {
        /// <summary>
        /// Gets the forecast asynchronous.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The forecast for the location</returns>
        Task<Forecast> GetForecastAsync(Location location, CancellationToken cancellationToken);
    }
}