namespace DarkSky.Ui.Desktop.Components.Tiles
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Icon name to image source converter
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter"/>
    public class IconNameToImageConverter : IValueConverter
    {
        /// <summary>
        /// The file name lookup based on https://erikflowers.github.io/weather-icons/
        /// </summary>
        private static readonly Dictionary<string, string> fileNameLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // if used in markup the following format is sufficient: &#xF002;
            ["cloudy"] = "F002",
            ["clear-day"] = "F00D",
            ["clear-night"] = "F02E",
            ["rain"] = "F008",
            ["snow"] = "F00A",
            ["sleet"] = "F0B2",
            ["wind"] = "F085",
            ["fog"] = "F003",
            ["partly-cloudy-day"] = "F002",
            ["partly-cloudy-night"] = "F083",
            ["hail"] = "F004",
            ["thunderstorm"] = "F010",
            ["tornado"] = "F056",
        };

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string iconName && targetType == typeof(string) && !string.IsNullOrWhiteSpace(iconName))
            {
                if (fileNameLookup.TryGetValue(iconName, out string iconChar)
                    && int.TryParse(iconChar, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int charCode))
                {
                    return (string)char.ConvertFromUtf32(charCode);
                }

                // defaults to unknown
                return "?";
            }

            return null;
        }

        /// <summary>
        /// Always throws not implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}