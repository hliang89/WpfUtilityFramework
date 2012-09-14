using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfUtilityFramework.Foundation
{
    public static class DataErrorInfoExtension
    {
        public static string Validate(this IDataErrorInfo instance)
        {
            if (instance == null) { throw new ArgumentNullException("instance"); }
            return instance.Error ?? "";
        }

        public static string Validate(this IDataErrorInfo instance, string memberName)
        {
            if (instance == null) { throw new ArgumentNullException("instance"); }
            return instance[memberName] ?? "";
        }
    }
}
