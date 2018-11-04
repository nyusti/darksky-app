namespace DarkSky.Ui.Desktop
{
    using System.Threading;
    using DarkSky.Application.Injection;
    using DarkSky.Ui.Desktop.Navigation;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Static applicaiton context
    /// </summary>
    public static class ApplicationContext
    {
        /// <summary>
        /// Gets or sets the application configuration.
        /// </summary>
        /// <value>The application configuration.</value>
        public static IConfiguration ApplicationConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the dependency resolver.
        /// </summary>
        /// <value>The dependency resolver.</value>
        public static IDependencyResolver DependencyResolver { get; set; }

        /// <summary>
        /// Gets the main cancellation token source.
        /// </summary>
        /// <value>The main cancellation token source.</value>
        public static CancellationTokenSource MainCancellationTokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Gets the navigation context. Stores state for the navigation mechanism
        /// </summary>
        /// <value>The navigation context.</value>
        public static NavigationContext NavigationContext { get; } = new NavigationContext();

        /// <summary>
        /// Gets the user context.
        /// </summary>
        /// <value>The user context.</value>
        public static UserContext UserContext { get; } = new UserContext();
    }
}