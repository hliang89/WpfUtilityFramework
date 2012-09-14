using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace WpfUtilityFramework.Applications
{
    public abstract class ViewModel:DataModel
    {
        private readonly IView view;

        protected ViewModel(IView view)
        {
            if (view == null) { throw new ArgumentException("view"); }
            this.view = view;

            if (SynchronizationContext.Current is DispatcherSynchronizationContext)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate() { this.view.DataContext = this; });
            }
            else
            {
                view.DataContext = this;
            }
        }

        public IView View { get { return view; } }
    }
}
