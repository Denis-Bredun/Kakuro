using System.Globalization;
using System.Windows.Data;

namespace Kakuro.Converters
{
    public class SumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || values[0] == null || values[1] == null)
                return string.Empty;

            string sumRight = values[0].ToString();
            string sumBottom = values[1].ToString();

            string additionalSpaces = GetSpaces(sumBottom);

            return $"╲{sumRight}\n{sumBottom}{additionalSpaces}╲";
        }

        private string GetSpaces(string sumBottom)
        {
            int defaultSpaces = 4;
            int spaceReductionFactor = 2;

            int spaces = defaultSpaces - spaceReductionFactor * sumBottom.Length;
            return spaces > 0 ? new string(' ', spaces) + " " : " ";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
