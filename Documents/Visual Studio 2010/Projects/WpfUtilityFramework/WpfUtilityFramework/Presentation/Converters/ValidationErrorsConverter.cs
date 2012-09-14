using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Globalization;

namespace WpfUtilityFramework.Presentation.Converters
{
    public class ValidationErrorsConverter:IValueConverter
    {
        private static readonly ValidationErrorsConverter defaultInstance = new ValidationErrorsConverter();

        public static ValidationErrorsConverter Default { get { return defaultInstance; } }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<ValidationError> validationErrors = value as IEnumerable<ValidationError>;
            if (validationErrors != null)
            {
                return string.Join(Environment.NewLine, validationErrors.Select(x => x.ErrorContent as string).Where(c => !string.IsNullOrEmpty(c)));
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
