using System.Globalization;
using System.Windows.Data;

namespace Kakuro.Converters
{
    public class MultiOrBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isInverted = parameter is bool invert && invert;

            if (values.Length == 2 &&
                values[0] is bool isGameCompleted &&
                values[1] is bool showCorrectAnswers)
            {
                bool result = isInverted ? (isGameCompleted || showCorrectAnswers) :
                                           (!isGameCompleted && !showCorrectAnswers);

                return result;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
