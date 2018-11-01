namespace DarkSky.Ui.Desktop.Components.Main
{
    using System.Windows;
    using Unity.Attributes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public MainWindowViewModel ViewModel
        {
            get => this.DataContext as MainWindowViewModel;
            set => this.DataContext = value;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}