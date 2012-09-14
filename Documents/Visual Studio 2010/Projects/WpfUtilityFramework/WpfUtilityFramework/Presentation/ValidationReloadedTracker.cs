using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfUtilityFramework.Presentation
{
    internal class ValidationReloadedTracker
    {
        private readonly ValidationTracker validationTracker;
        private readonly IEnumerable<ValidationError> errors;

        public ValidationReloadedTracker(ValidationTracker validationTracker, object validationSource,
            IEnumerable<ValidationError> errors)
        {
            this.validationTracker = validationTracker;
            this.errors = errors;

            if (validationSource is FrameworkElement)
            {
                ((FrameworkElement)validationSource).Loaded += ValidationSourceLoaded;
            }
            else
            {
                ((FrameworkContentElement)validationSource).Loaded += ValidationSourceLoaded;
            }
        }


        private void ValidationSourceLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement)
            {
                ((FrameworkElement)sender).Loaded -= ValidationSourceLoaded;
            }
            else
            {
                ((FrameworkContentElement)sender).Loaded -= ValidationSourceLoaded;
            }

            validationTracker.AddErrors(sender, errors);
        }
    }
}
