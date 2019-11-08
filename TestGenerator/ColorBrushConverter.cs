using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TestGenerator
{
    class ColorBrushConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool ans)
            {
                return ans ? Brushes.LightGreen : Brushes.Red;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
