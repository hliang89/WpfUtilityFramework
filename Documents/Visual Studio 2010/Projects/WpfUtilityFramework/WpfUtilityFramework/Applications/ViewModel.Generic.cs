using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications
{
    public abstract class ViewModel<TView>:ViewModel where TView:IView
    {
        private readonly IView view;

        protected ViewModel(TView view)
            : base(view)
        {
            this.view = view;
        }

        protected IView View { get { return view; } }
    }
}
