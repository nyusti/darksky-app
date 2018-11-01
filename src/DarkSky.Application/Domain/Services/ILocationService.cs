namespace DarkSky.Application.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;

    /// <summary>
    /// Location service interface
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Gets the favorite locations asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of locations</returns>
        Task<IList<Location>> GetFavoriteLocationsAsync(CancellationToken cancellationToken);
    }
}