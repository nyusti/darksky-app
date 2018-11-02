namespace DarkSky.Ui.Desktop.Components.Tiles
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for CurrentWeatherTitle.xaml
    /// </summary>
    public partial class CurrentWeatherTile : UserControl
    {
        public CurrentWeatherTile()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon. This enables animation, styling,
        // binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(CurrentWeatherTile), new PropertyMetadata());
    }
}