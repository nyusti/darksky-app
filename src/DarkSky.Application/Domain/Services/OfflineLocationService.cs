namespace DarkSky.Application.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;

    public class OfflineLocationService : ILocationService
    {
        public Task<List<Location>> GetFavoriteLocationsAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<Location>
            {
                new Location()
            });
        }
    }
}