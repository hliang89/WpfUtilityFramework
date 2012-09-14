using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;

namespace WpfUtilityFramework.Applications
{
    public static class ViewHelper
    {
        public static ViewModel GetViewModel(this IView view)
        {
            if (view == null) { throw new ArgumentException("view"); }

            object dataContext = view.DataContext;

            if (dataContext == null && SynchronizationContext.Current is DispatcherSynchronizationContext)
            {
                DispatcherHelper.DoEvents();
                dataContext = view.DataContext;
            }
            return dataContext as ViewModel;
        }
    }
}
