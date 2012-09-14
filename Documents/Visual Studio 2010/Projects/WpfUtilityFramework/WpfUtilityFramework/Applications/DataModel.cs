using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfUtilityFramework.Foundation;
using System.ComponentModel;
using System.Collections.Specialized;

namespace WpfUtilityFramework.Applications
{
    public abstract class DataModel:Model
    {
        private readonly List<PropertyChangedEventListener> propertyEventListeners= new List<PropertyChangedEventListener>();
        private readonly List<CollectionChangedEventListener> collectionEventListeners=new List<CollectionChangedEventListener>();

        protected void AddWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentException("source"); }
            if (handler == null) { throw new ArgumentException("handler"); }

            PropertyChangedEventListener listener = new PropertyChangedEventListener(source, handler);

            propertyEventListeners.Add(listener);

            PropertyChangedEventManager.AddListener(source, listener, "");
        }

        protected void RemoveWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
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

        protected void AddWeakEventListner(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentException("source"); }
            if (handler == null) { throw new ArgumentException("handler"); }

            CollectionChangedEventListener listener = new CollectionChangedEventListener(source, handler);

            collectionEventListeners.Add(listener);

            CollectionChangedEventManager.AddListener(source, listener);
        }

        protected void RemoveWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
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
