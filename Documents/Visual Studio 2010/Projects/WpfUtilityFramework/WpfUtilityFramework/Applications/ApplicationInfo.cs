using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Reflection;

namespace WpfUtilityFramework.Applications
{
    public class ApplicationInfo
    {
        public static string productName;
        public static bool productNameCached;
        public static string productCompany;
        public static bool productCompanyCached;
        public static string copyright;
        public static bool copyrightCached;
        public static string applicationPath;
        public static bool applicationPathCached;

        public static string ProductName
        {
            get
            {
                if (!productNameCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        AssemblyProductAttribute productAttribute = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(entryAssembly,
                            typeof(AssemblyProductAttribute)));
                        productName = (productAttribute != null) ? productAttribute.Product : "";
                    }
                    else
                    {
                        productName = "";
                    }
                    productNameCached = true;
                }
                return productName;
            }
        }

        public static string Company
        {
            get
            {
                if (!productCompanyCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        AssemblyCompanyAttribute companyAttribute = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(
                        entryAssembly, typeof(AssemblyCompanyAttribute)));

                        productCompany = (companyAttribute != null) ? companyAttribute.Company : "";
                    }
                    else
                    {
                        productCompany = "";
                    }
                    productCompanyCached = true;
                }
                return productCompany;
            }
        }

        public static string Copyright
        {
            get
            {
                if (!copyrightCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();

                    if (entryAssembly != null)
                    {
                        AssemblyCopyrightAttribute copyrightAttribute = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute
                            (entryAssembly, typeof(AssemblyCopyrightAttribute)));

                        copyright = (copyrightAttribute!=null)?copyrightAttribute.Copyright:"";
                    }
                    else
                    {
                        copyright = "";
                    }
                    copyrightCached = true;
                }
                return copyright;
            }
        }
    }
}
