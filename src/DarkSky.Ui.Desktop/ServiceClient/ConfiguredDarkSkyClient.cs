namespace DarkSky.Ui.Desktop.ServiceClient
{
    using System;
    using DarkSky.Client;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Configured dark sky client
    /// </summary>
    /// <seealso cref="DarkSky.Client.DarkSkyClient"/>
    public class ConfiguredDarkSkyClient : DarkSkyClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfiguredDarkSkyClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ConfiguredDarkSkyClient(IConfiguration configuration)
            : base(
                  baseUri: configuration.GetValue<Uri>("DarkSky:BaseUri"),
                  credentials: new SecretKeyCredentials(configuration.GetValue<string>("DarkSky:Key")))
        {
        }
    }
}