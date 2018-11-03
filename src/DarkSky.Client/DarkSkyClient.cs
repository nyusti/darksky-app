namespace DarkSky.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Dark Sky clinet implementation
    /// </summary>
    /// <seealso cref="Microsoft.Rest.ServiceClient{DarkSky.Client.DarkSkyClient}"/>
    /// <seealso cref="DarkSky.Client.IDarkSkyClient"/>
    public partial class DarkSkyClient : ServiceClient<DarkSkyClient>, IDarkSkyClient
    {
        /// <inheritdoc/>
        public Uri BaseUri { get; set; }

        /// <inheritdoc/>
        public JsonSerializerSettings SerializationSettings { get; private set; }

        /// <inheritdoc/>
        public JsonSerializerSettings DeserializationSettings { get; private set; }

        /// <inheritdoc/>
        public ServiceClientCredentials Credentials { get; private set; }

        /// <inheritdoc/>
        public IForecastOperations ForecastOperations { get; private set; }

        protected DarkSkyClient(params DelegatingHandler[] handlers)
            : base(handlers)
        {
            this.Initialize();
        }

        protected DarkSkyClient(HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
            : base(rootHandler, handlers)
        {
            this.Initialize();
        }

        protected DarkSkyClient(Uri baseUri, params DelegatingHandler[] handlers)
            : this(handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        protected DarkSkyClient(Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
            : this(rootHandler, handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public DarkSkyClient(ServiceClientCredentials credentials, params DelegatingHandler[] handlers)
            : this(handlers)
        {
            this.Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        public DarkSkyClient(ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
            : this(rootHandler, handlers)
        {
            this.Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkSkyClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="handlers">The handlers.</param>
        /// <exception cref="ArgumentNullException">baseUri or credentials is null</exception>
        public DarkSkyClient(Uri baseUri, ServiceClientCredentials credentials, params DelegatingHandler[] handlers)
            : this(handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            this.Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkSkyClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="rootHandler">The root handler.</param>
        /// <param name="handlers">The handlers.</param>
        /// <exception cref="ArgumentNullException">baseUri or credentials is null</exception>
        public DarkSkyClient(Uri baseUri, ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
            : this(rootHandler, handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            this.Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            if (this.Credentials != null)
            {
                this.Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Place to run custom initialization logic
        /// </summary>
        partial void CustomInitialize();

        private void Initialize()
        {
            this.ForecastOperations = new ForecastOperations(this);
            this.BaseUri = new Uri("https://api.darksky.net/forecast/");
            this.SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new UnixDateTimeConverter()
                }
            };

            this.SerializationSettings.Converters.Add(new TransformationJsonConverter());
            this.DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new UnixDateTimeConverter()
                }
            };

            this.CustomInitialize();
            this.DeserializationSettings.Converters.Add(new TransformationJsonConverter());
        }
    }
}