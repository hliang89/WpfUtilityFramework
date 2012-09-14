using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Collections.Specialized;

namespace WpfUtilityFramework.Applications
{
    public class CollectionChangedEventListener:IWeakEventListener
    {
        private readonly NotifyCollectionChangedEventHandler handler;
        private readonly INotifyCollectionChanged source;

        public CollectionChangedEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (handler == null) { throw new ArgumentException("handler"); }
            if (source == null) { throw new ArgumentException("source"); }
            this.source = source;
            this.handler = handler;
        }

        public NotifyCollectionChangedEventHandler Handler { get { return handler; } }
        public INotifyCollectionChanged Source { get { return source; } }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            handler(sender, (NotifyCollectionChangedEventArgs)e);
            return true;
        }
    }
}
