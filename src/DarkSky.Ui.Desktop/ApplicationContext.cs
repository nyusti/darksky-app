namespace DarkSky.Ui.Desktop
{
    using System.Threading;
    using DarkSky.Application.Injection;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Static applicaiton context
    /// </summary>
    public static class ApplicationContext
    {
        /// <summary>
        /// Gets the main cancellation token source.
        /// </summary>
        /// <value>The main cancellation token source.</value>
        public static CancellationTokenSource MainCancellationTokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Gets or sets the dependency resolver.
        /// </summary>
        /// <value>The dependency resolver.</value>
        public static IDependencyResolver DependencyResolver { get; set; }

        /// <summary>
        /// Gets or sets the application configuration.
        /// </summary>
        /// <value>The application configuration.</value>
        public static IConfiguration ApplicationConfiguration { get; set; }
    }
}