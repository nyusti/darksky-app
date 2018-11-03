namespace DarkSky.Ui.Desktop.Navigation
{
    using System;
    using System.Reactive.Linq;
    using System.Windows.Navigation;
    using DarkSky.Application.Injection;
    using GalaSoft.MvvmLight.Views;

    public class PageNavigationService : INavigationService
    {
        private readonly NavigationService navigationService;
        private IDependencyScope currentScope;

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

        public static object ExtraData { get; private set; }

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

        public void GoBack()
        {
            this.navigationService.GoBack();
        }

        public void NavigateTo(string pageKey)
        {
            this.navigationService.Navigate(new Uri(pageKey, UriKind.RelativeOrAbsolute));
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            this.navigationService.Navigate(new Uri(pageKey, UriKind.RelativeOrAbsolute), parameter);
            ExtraData = parameter;
        }
    }
}