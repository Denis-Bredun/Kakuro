using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Data;

namespace Kakuro.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var enumValue = value.ToString();
            var field = value.GetType().GetField(enumValue);
            var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute));

            return attribute != null ? attribute.Value : enumValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
