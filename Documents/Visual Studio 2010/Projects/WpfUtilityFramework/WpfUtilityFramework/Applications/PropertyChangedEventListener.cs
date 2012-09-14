using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace WpfUtilityFramework.Applications
{
    internal class PropertyChangedEventListener:IWeakEventListener
    {
        private readonly INotifyPropertyChanged source;
        private readonly PropertyChangedEventHandler handler;

        public PropertyChangedEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null){throw new ArgumentException("source");}
            if (handler == null){throw new ArgumentException ("handler");}
            this.source = source;
            this.handler = handler;
        }

        public INotifyPropertyChanged Source { get { return source; } }
        public PropertyChangedEventHandler Handler { get { return handler; } }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            handler(sender, (PropertyChangedEventArgs)e);
            return true;                        
        }
    }
}
