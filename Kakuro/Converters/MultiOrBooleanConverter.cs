using System.Globalization;
using System.Windows.Data;

namespace Kakuro.Converters
{
    public class MultiOrBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 &&
                values[0] is bool isGameCompleted &&
                values[1] is bool showCorrectAnswers)
            {
                return !isGameCompleted && !showCorrectAnswers;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
