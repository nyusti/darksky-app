using System;
using System.Reactive.Threading.Tasks;
using System.Threading;
using DarkSky.Application.Domain.Model;

namespace DarkSky.Application.Domain.Services
{
    public static class ForecastServiceExtensions
    {
        public static IObservable<Forecast> GetForecast(this IForecastService forecastService, Location location, CancellationToken cancellationToken)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return forecastService.GetForecastAsync(location, cancellationToken).ToObservable();
        }
    }
}