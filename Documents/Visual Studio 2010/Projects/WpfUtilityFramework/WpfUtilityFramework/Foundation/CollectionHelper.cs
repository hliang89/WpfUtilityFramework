using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Foundation
{
    public static class CollectionHelper
    {
        public static T GetNextElementOrDefault<T>(IEnumerable<T> collection, T current)
        {
            if (collection == null) { throw new ArgumentNullException("No such collection"); }

            bool found = false;
            IEnumerator<T> enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (EqualityComparer<T>.Default.Equals(enumerator.Current, current))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                throw new ArgumentNullException("The collection does not contain the current item");
            }

            if (enumerator.MoveNext())
            {
                return enumerator.Current;
            }
            else
            {
                return default(T);
            }
        }
    }
}
