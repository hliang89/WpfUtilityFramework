using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace WpfUtilityFramework
{
    public static class WafConfiguration
    {
        //private static readonly bool isInDesignMode = DesignerAttribute.IsDefined(

#if (DEBUG)
        private static bool debug = true;
#else
        private static bool debug = false;
#endif 

        public static bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }
    }
}
