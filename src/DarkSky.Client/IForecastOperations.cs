namespace DarkSky.Client
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Client.Models;
    using Microsoft.Rest;

    public partial interface IForecastOperations
    {
        Task<HttpOperationResponse<Forecast>> GetForecastWithHttpMessagesAsync(double latitude, double longitude, string lang = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}