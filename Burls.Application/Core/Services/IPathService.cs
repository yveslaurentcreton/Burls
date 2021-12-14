using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public interface IPathService
    {
        string ApplicationDataFolder { get; }
        string ConfigurationsFolder { get; }
        string SettingsFileName { get; }
        string BrowsersFileName { get; }
    }
}
