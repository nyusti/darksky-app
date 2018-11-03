namespace DarkSky.Application.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;

    /// <summary>
    /// Offline (in-memory) location provider service
    /// </summary>
    /// <seealso cref="DarkSky.Application.Domain.Services.ILocationService"/>
    public class OfflineLocationService : ILocationService
    {
        // Budapest, Luxembourg, Debrecen, Pécs, Wienna, Prague, München, Amsterdam.
        private static readonly List<Location> inMemoryLocationStore = new List<Location>
        {
            new Location { City = "Amsterdam", CountryCode = "NL", Latitude = 52.3745,  Longiture = 4.898 },
            new Location { City = "Budapest", CountryCode = "HU", Latitude = 47.4984, Longiture = 19.0405 },
            new Location { City = "Debrecen", CountryCode = "HU", Latitude =  47.5314, Longiture = 21.626 },
            new Location { City = "Pécs", CountryCode = "HU", Latitude =  46.0763, Longiture = 18.2281 },
            new Location { City = "Wienna", CountryCode = "AT", Latitude =  48.2084,Longiture =  16.3725 },
            new Location { City = "Prague", CountryCode = "CZ", Latitude =  50.0875, Longiture = 14.4213 },
            new Location { City = "Munchen", CountryCode = "DE", Latitude =  50.0875, Longiture = 14.4213 }
        };

        /// <inheritdoc/>
        public Task<List<Location>> GetFavoriteLocationsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(inMemoryLocationStore.OrderBy(p => p.City).ToList());
        }
    }
}