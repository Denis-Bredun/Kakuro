using System.Globalization;
using System.Windows.Data;

namespace Kakuro.Converters
{
    public class WindowWidthToTabItemWidthConverter : IValueConverter
    {
        private const int UNDISPLAYABLE_DPI = 10;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double windowWidth)
                return (windowWidth / 2) - UNDISPLAYABLE_DPI;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
