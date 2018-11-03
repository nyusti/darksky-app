namespace DarkSky.Ui.Desktop.Components.Error
{
    using System.Windows;
    using Unity.Attributes;

    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        [Dependency]
        public ErrorWindowViewModel ViewModel
        {
            get => this.DataContext as ErrorWindowViewModel;
            set => this.DataContext = value;
        }

        public ErrorWindow()
        {
            this.InitializeComponent();
        }
    }
}