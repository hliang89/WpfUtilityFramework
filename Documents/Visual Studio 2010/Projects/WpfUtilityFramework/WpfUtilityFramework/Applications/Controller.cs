using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WpfUtilityFramework.Applications
{
    public class Controller
    {
        private readonly List<PropertyChangedEventListener> propertyEventListeners = new List<PropertyChangedEventListener>();
        private readonly List<CollectionChangedEventListener> collectionEventListeners = new List<CollectionChangedEventListener>();

        protected void AddEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentException("source"); }
            if (handler == null) { throw new ArgumentException("handler"); }

            PropertyChangedEventListener listener = new PropertyChangedEventListener(source, handler);

            propertyEventListeners.Add(listener);

            PropertyChangedEventManager.AddListener(source, listener, "");
        }

        protected void RemoveEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentException("source"); }
            if (handler == null) { throw new ArgumentException("handler"); }

            PropertyChangedEventListener listener = propertyEventListeners.LastOrDefault(c => c.Source == source && c.Handler == handler);

            if (listener != null)
            {
                propertyEventListeners.Remove(listener);
                PropertyChangedEventManager.RemoveListener(source, listener, "");
            }
        }

        protected void AddEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentException("source"); }
            if (handler == null) { throw new ArgumentException("handler"); }

            CollectionChangedEventListener listener = new CollectionChangedEventListener(source, handler);

            collectionEventListeners.Add(listener);

            CollectionChangedEventManager.AddListener(source, listener);
        }

        protected void RemoveEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentException("source"); }
            if (handler == null) { throw new ArgumentException("handler"); }

            CollectionChangedEventListener listener = collectionEventListeners.LastOrDefault(c => c.Source == source && c.Handler == handler);

            if (listener != null)
            {
                collectionEventListeners.Remove(listener);
                CollectionChangedEventManager.RemoveListener(source, listener);
            }
        }
    }
}
