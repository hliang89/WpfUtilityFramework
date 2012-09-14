using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications.Services
{
    public interface IMessageService
    {
        void ShowMessage(object owner, string message);

        void ShowError(object owner, string message);

        void ShowWarning(object owner, string message);

        bool? ShowQuestion(object owner, string message);

        bool ShowYesNoQuestion(object owner, string message);
    }
}
