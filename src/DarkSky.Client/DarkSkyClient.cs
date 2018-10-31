namespace DarkSky.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;

    public partial class DarkSkyClient : ServiceClient<DarkSkyClient>, IDarkSkyClient
    {
        public Uri BaseUri { get; set; }

        public JsonSerializerSettings SerializationSettings { get; private set; }

        public JsonSerializerSettings DeserializationSettings { get; private set; }

        public ServiceClientCredentials Credentials { get; private set; }

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
                    new UnixTimeJsonConverter()
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
                    new UnixTimeJsonConverter()
                }
            };

            this.CustomInitialize();
            this.DeserializationSettings.Converters.Add(new TransformationJsonConverter());
        }
    }
}