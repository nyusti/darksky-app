namespace DarkSky.Ui.Desktop
{
    using System.Windows;
    using GalaSoft.MvvmLight.Threading;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the <see cref="App"/> class.
        /// </summary>
        static App()
        {
            DispatcherHelper.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.Startup += (s, args) =>
            {
                var bootstrapper = new Bootstrapper();
                bootstrapper.Run(this);
            };
        }
    }
}