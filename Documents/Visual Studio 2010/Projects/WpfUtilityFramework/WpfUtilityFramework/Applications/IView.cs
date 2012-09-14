using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications
{
    public interface IView
    {
        object DataContext { get; set; }
    }
}
