using System;
using System.ComponentModel;
using System.Globalization;

namespace WpfUtilityFramework.Foundation
{
    [Serializable]
    public abstract class Model:INotifyPropertyChanged
    {
        [NonSerialized]
        private PropertyChangedEventHandler propertyChanged;
            
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (WafConfiguration.Debug) { CheckPropertyName(propertyName); }
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (propertyChanged != null) { propertyChanged(this, e); }
        }

        protected void CheckPropertyName(string propertyName)
        {
            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)[propertyName];
            if (propertyDescriptor == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The property with the property name {0} does not exist.", propertyName));
            }
        }
    }
}
