namespace DarkSky.Client
{
    using System;
    using Microsoft.Rest;
    using Newtonsoft.Json;

    /// <summary>
    /// Dark Sky cient interface
    /// </summary>
    public partial interface IDarkSkyClient
    {
        /// <summary>
        /// Gets or sets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        Uri BaseUri { get; set; }

        /// <summary>
        /// Gets the serialization settings.
        /// </summary>
        /// <value>The serialization settings.</value>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets the deserialization settings.
        /// </summary>
        /// <value>The deserialization settings.</value>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        ServiceClientCredentials Credentials { get; }

        /// <summary>
        /// Gets the forecast operations.
        /// </summary>
        /// <value>The forecast operations.</value>
        IForecastOperations ForecastOperations { get; }
    }
}