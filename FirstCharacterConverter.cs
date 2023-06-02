using System.Globalization;
using System.Windows.Data;

namespace filmio {
    internal class FirstCharacterConverter : IValueConverter {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string? strVal = value as string;
            if (!string.IsNullOrEmpty(strVal)) {
                return strVal[0].ToString();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            
            throw new NotSupportedException();
        }
    }
}
