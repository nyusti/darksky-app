using System.Windows.Controls;
using Unity.Attributes;

namespace DarkSky.Ui.Desktop.Components.Tiles
{
    /// <summary>
    /// Interaction logic for CurrentWeatherPage.xaml
    /// </summary>
    public partial class CurrentWeatherPage : Page
    {
        public CurrentWeatherPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Dependency]
        public CurrentWeatherPageViewModel ViewModel
        {
            get => this.DataContext as CurrentWeatherPageViewModel;
            set => this.DataContext = value;
        }
    }
}