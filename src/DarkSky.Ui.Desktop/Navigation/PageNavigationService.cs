namespace DarkSky.Ui.Desktop.Navigation
{
    using System;
    using System.Reactive.Linq;
    using System.Windows.Navigation;
    using DarkSky.Application.Injection;
    using GalaSoft.MvvmLight.Views;

    /// <summary>
    /// Simple page navigation service
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.Views.INavigationService"/>
    public class PageNavigationService : INavigationService
    {
        private readonly NavigationService navigationService;
        private IDependencyScope currentScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageNavigationService"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service to wrap.</param>
        /// <exception cref="ArgumentNullException">navigationService is null</exception>
        public PageNavigationService(NavigationService navigationService)
        {
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.OnNavigating.Subscribe(args =>
            {
                this.currentScope?.Dispose();
            });

            this.OnLoaded.Subscribe(arg =>
            {
                var scope = ApplicationContext.DependencyResolver.BeginScope();
                scope.BuildUp(arg.Content.GetType(), arg.Content);
                this.CurrentPageKey = arg.Uri.ToString();
                this.currentScope = scope;
            });
        }

        /// <inheritdoc/>
        public string CurrentPageKey { get; private set; }

        private IObservable<NavigationEventArgs> OnLoaded
        {
            get
            {
                return Observable
                    .FromEventPattern<LoadCompletedEventHandler, NavigationEventArgs>(
                        h => this.navigationService.LoadCompleted += h,
                        h => this.navigationService.LoadCompleted -= h)
                    .Select(x => x.EventArgs);
            }
        }

        private IObservable<NavigatingCancelEventArgs> OnNavigating
        {
            get
            {
                return Observable
                    .FromEventPattern<NavigatingCancelEventHandler, NavigatingCancelEventArgs>(
                        h => this.navigationService.Navigating += h,
                        h => this.navigationService.Navigating -= h)
                    .Select(x => x.EventArgs);
            }
        }

        /// <inheritdoc/>
        public void GoBack()
        {
            this.navigationService.GoBack();
        }

        /// <inheritdoc/>
        public void NavigateTo(string pageKey)
        {
            if (string.IsNullOrWhiteSpace(pageKey))
            {
                throw new ArgumentException("pageKey is null or empty", nameof(pageKey));
            }

            this.NavigateTo(pageKey, null);
        }

        /// <inheritdoc/>
        public void NavigateTo(string pageKey, object parameter)
        {
            if (string.IsNullOrWhiteSpace(pageKey))
            {
                throw new ArgumentException("pageKey is null or empty", nameof(pageKey));
            }

            // if the page key is the same as the current one, just do a refresh

            this.navigationService.Navigate(new Uri(pageKey, UriKind.RelativeOrAbsolute), parameter);
            ApplicationContext.NavigationContext.NavigationState = parameter;
        }
    }
}