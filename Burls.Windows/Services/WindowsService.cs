using Burls.Application.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class WindowsService : IOperatingSystemService
    {
        public void OpenColorSettings()
        {
            Process.Start(new ProcessStartInfo("ms-settings:colors") { UseShellExecute = true });
        }
    }
}
