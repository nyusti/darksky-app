namespace DarkSky.Ui.Desktop.Components.Error
{
    using System.Linq;

    /// <summary>
    /// Error window view model
    /// </summary>
    /// <seealso cref="DarkSky.Ui.Desktop.Components.ComponentViewModel"/>
    public class ErrorWindowViewModel : ComponentViewModel
    {
        private string exceptionMessage;

        public string ExceptionMessage
        {
            get => this.exceptionMessage;
            set => this.Set(() => this.ExceptionMessage, ref this.exceptionMessage, value);
        }

        /// <inheritdoc/>
        public override void Init()
        {
            // gets the last exception from the usercontext
            this.ExceptionMessage = ApplicationContext.UserContext.LastExceptions.LastOrDefault()?.ToString();
            base.Init();
        }
    }
}