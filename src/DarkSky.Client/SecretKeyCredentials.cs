namespace DarkSky.Client
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;

    /// <summary>
    /// Secret key credentials
    /// </summary>
    /// <seealso cref="Microsoft.Rest.ServiceClientCredentials"/>
    public class SecretKeyCredentials : ServiceClientCredentials
    {
        private readonly string secretKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretKeyCredentials"/> class.
        /// </summary>
        /// <param name="secretKey">The secret key.</param>
        /// <exception cref="ArgumentException">Secret key is null or empty - secretKey</exception>
        public SecretKeyCredentials(string secretKey)
        {
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new ArgumentException("Secret key is null or empty", nameof(secretKey));
            }

            this.secretKey = secretKey;
        }

        /// <inheritdoc/>
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.RequestUri = new Uri(request.RequestUri.ToString().Replace("{key}", this.secretKey));
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}