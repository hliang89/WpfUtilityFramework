using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace WpfUtilityFramework.Presentation.Converters
{
    [ValueConversion(typeof(object), typeof(string))]
    public sealed class StringFormatConverter:IValueConverter
    {
        private static readonly StringFormatConverter defaultInstance = new StringFormatConverter();

        public static StringFormatConverter Default { get { return defaultInstance; } }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string format = parameter as string ?? "{0}";
            return string.Format(culture, format, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
