using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfUtilityFramework.Applications
{
    public interface IModuleController
    {
        void Init();

        void Run();

        void Shutdown();
    }
}
