namespace DarkSky.Ui.Desktop
{
    using DarkSky.Application.Injection;
    using DarkSky.Ui.Desktop.Components.Error;
    using DarkSky.Ui.Desktop.Components.Main;
    using DarkSky.Ui.Desktop.Configuration;
    using Microsoft.Extensions.Configuration;
    using Unity;

    /// <summary>
    /// Application bootstrapper
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run(App application)
        {
            // register global exception handling
            application.DispatcherUnhandledException += (source, args) =>
            {
                ApplicationContext.UserContext.LastExceptions.Add(args.Exception);
                using (var windowScope = ApplicationContext.DependencyResolver.BeginScope())
                {
                    var errorWindow = windowScope.Resolve<ErrorWindow>();
                    errorWindow.ShowDialog();
                }

                args.Handled = true;
            };

            // load configuration from file
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // register configuration into container and add reference to applicaiton context
            UnityConfiguration.Container.RegisterInstance<IConfiguration>(configuration);
            ApplicationContext.ApplicationConfiguration = UnityConfiguration.Container.Resolve<IConfiguration>();

            // set the default dependency resolver
            ApplicationContext.DependencyResolver = UnityConfiguration.Container.Resolve<IDependencyResolver>();

            var mainScope = ApplicationContext.DependencyResolver.BeginScope();

            // setup cleanup code on exit
            application.Exit += (source, args) =>
            {
                mainScope.Dispose();

                if (ApplicationContext.MainCancellationTokenSource?.IsCancellationRequested == false)
                {
                    ApplicationContext.MainCancellationTokenSource.Cancel();
                    ApplicationContext.MainCancellationTokenSource.Dispose();
                }

                ApplicationContext.DependencyResolver.Dispose();
            };

            // open the main window
            application.MainWindow = mainScope.Resolve<MainWindow>();
            application.MainWindow.ShowActivated = true;
            application.MainWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            application.MainWindow.Show();
        }
    }
}