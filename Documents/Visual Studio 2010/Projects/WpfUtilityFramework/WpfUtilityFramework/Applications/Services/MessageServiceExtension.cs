using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications.Services
{
    public static class MessageServiceExtension
    {
        public static void ShowMessage(this IMessageService service, string message)
        {
            if (service == null) { throw new ArgumentException("service"); }
            service.ShowMessage(null, message);
        }

        public static void ShowError(this IMessageService service, string message)
        {
            if (service == null) { throw new ArgumentException("serivce"); }
            service.ShowError(null, message);
        }

        public static void ShowWarning(this IMessageService service, string message)
        {
            if (service == null) { throw new ArgumentException("service"); }
            service.ShowWarning(null, message);
        }
        public static bool? ShowQuestion(this IMessageService service, string message)
        {
            if (service == null) { throw new ArgumentException("service"); }
            return service.ShowQuestion(null, message);
        }

        public static void ShowYesNoQuestion(this IMessageService service, string message)
        {
            if (service == null) { throw new ArgumentException("service"); }
            service.ShowYesNoQuestion(null, message);
        }
    }
}
