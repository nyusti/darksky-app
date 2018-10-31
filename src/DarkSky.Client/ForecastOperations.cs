namespace DarkSky.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using DarkSky.Client.Models;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;

    internal partial class ForecastOperations : IServiceOperations<DarkSkyClient>, IForecastOperations
    {
        public ForecastOperations(DarkSkyClient darkSkyClient)
        {
            this.Client = darkSkyClient ?? throw new ArgumentNullException(nameof(darkSkyClient));
        }

        public DarkSkyClient Client { get; }

        public async Task<HttpOperationResponse<Forecast>> GetForecastWithHttpMessagesAsync(double latitude, double longitude, string lang = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Tracing
            var shouldTrace = ServiceClientTracing.IsEnabled;
            string invocationId = null;
            if (shouldTrace)
            {
                invocationId = ServiceClientTracing.NextInvocationId.ToString();
                var tracingParameters = new Dictionary<string, object>
                {
                    { "latitude", latitude },
                    { "longitude", longitude },
                    { "lang", lang },
                    { "cancellationToken", cancellationToken }
                };

                ServiceClientTracing.Enter(invocationId, this, "GetForecast", tracingParameters);
            }

            // Construct URL
            var baseUrl = Client.BaseUri.AbsoluteUri;
            var url = new Uri(new Uri(baseUrl + (baseUrl.EndsWith("/") ? "" : "/")), "{key}/{latitude},{longitude}").ToString();
            url = url.Replace("{latitude}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(latitude, this.Client.SerializationSettings).Trim('"')));
            url = url.Replace("{longitude}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(longitude, this.Client.SerializationSettings).Trim('"')));

            var queryParameters = new List<string>();
            if (!string.IsNullOrWhiteSpace(lang))
            {
                queryParameters.Add(string.Format("lang={0}", Uri.EscapeDataString(lang)));
            }

            if (queryParameters.Count > 0)
            {
                url += (url.Contains("?") ? "&" : "?") + string.Join("&", queryParameters);
            }

            // Create HTTP transport objects
            var httpRequest = new HttpRequestMessage();
            HttpResponseMessage httpResponse = null;
            httpRequest.Method = new HttpMethod("GET");
            httpRequest.RequestUri = new Uri(url);

            if (customHeaders != null)
            {
                foreach (var header in customHeaders)
                {
                    if (httpRequest.Headers.Contains(header.Key))
                    {
                        httpRequest.Headers.Remove(header.Key);
                    }

                    httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            // Serialize Request
            string requestContent = null;

            // Set Credentials
            if (this.Client.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await this.Client.Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }

            // Send Request
            if (shouldTrace)
            {
                ServiceClientTracing.SendRequest(invocationId, httpRequest);
            }

            cancellationToken.ThrowIfCancellationRequested();
            httpResponse = await this.Client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

            if (shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(invocationId, httpResponse);
            }

            var statusCode = httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string responseContent = null;
            if ((int)statusCode != 200)
            {
                var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'", statusCode));
                try
                {
                    responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                catch (JsonException)
                {
                    // Ignore the exception
                }

                ex.Body = responseContent;
                ex.Request = new HttpRequestMessageWrapper(httpRequest, requestContent);
                ex.Response = new HttpResponseMessageWrapper(httpResponse, responseContent);

                if (shouldTrace)
                {
                    ServiceClientTracing.Error(invocationId, ex);
                }

                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }

                throw ex;
            }

            // Create Result
            var result = new HttpOperationResponse<Forecast>();
            result.Request = httpRequest;
            result.Response = httpResponse;

            // Deserialize Response
            if ((int)statusCode == 200)
            {
                responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    result.Body = SafeJsonConvert.DeserializeObject<Forecast>(responseContent, Client.DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    httpRequest.Dispose();
                    if (httpResponse != null)
                    {
                        httpResponse.Dispose();
                    }

                    throw new SerializationException("Unable to deserialize the response.", responseContent, ex);
                }
            }
            if (shouldTrace)
            {
                ServiceClientTracing.Exit(invocationId, result);
            }

            return result;
        }
    }
}