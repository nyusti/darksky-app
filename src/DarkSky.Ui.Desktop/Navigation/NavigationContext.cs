namespace DarkSky.Ui.Desktop.Navigation
{
    /// <summary>
    /// Query string support
    /// </summary>
    public class NavigationContext
    {
        /// <summary>
        /// Gets or sets the state of the navigation. Stores object passed to the navigation from the
        /// previous site.
        /// </summary>
        /// <value>The state of the navigation.</value>
        public object NavigationState { get; set; }
    }
}