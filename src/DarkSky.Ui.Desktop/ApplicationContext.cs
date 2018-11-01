namespace DarkSky.Ui.Desktop
{
    using System.Threading;
    using DarkSky.Application.Injection;

    public static class ApplicationContext
    {
        public static CancellationTokenSource MainCancellationTokenSource { get; } = new CancellationTokenSource();

        public static IDependencyResolver DependencyResolver { get; set; }
    }
}