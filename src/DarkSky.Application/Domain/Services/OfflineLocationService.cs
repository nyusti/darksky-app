namespace DarkSky.Application.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Application.Domain.Model;

    public class OfflineLocationService : ILocationService
    {
        public async Task<List<Location>> GetFavoriteLocationsAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(5000);
            return new List<Location>
            {
                new Location()
            };
        }
    }
}