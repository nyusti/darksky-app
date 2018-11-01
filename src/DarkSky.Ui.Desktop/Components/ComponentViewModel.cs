namespace DarkSky.Ui.Desktop.Components
{
    using System.Threading;
    using GalaSoft.MvvmLight;
    using Unity.Attributes;

    public abstract class ComponentViewModel : ViewModelBase
    {
        [Dependency]
        protected CancellationToken CancellationToken { get; private set; }

        [InjectionMethod]
        public virtual void Init()
        {
        }
    }
}