namespace DarkSky.Ui.Desktop.Components.Tiles
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Icon name to image source converter
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter"/>
    public class IconNameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string iconName && targetType == typeof(ImageSource) && !string.IsNullOrWhiteSpace(iconName))
            {
                switch (iconName.ToLowerInvariant())
                {
                    case "cloudy":
                        return new BitmapImage(new Uri("pack://application:,,,/DarkSky.Ui.Desktop;component/Content/Cloud.png"));
                    default:
                        break;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}