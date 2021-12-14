using Burls.Windows.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public class PathService : IPathService
    {
        private readonly AppSettings _appSettings;

        public string ApplicationDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Burls");
        public string ConfigurationsFolder => Path.Combine(ApplicationDataFolder, _appSettings.ConfigurationsFolder);
        public string SettingsFileName => Path.Combine(ConfigurationsFolder, _appSettings.SettingsFileName);
        public string BrowsersFileName => Path.Combine(ConfigurationsFolder, _appSettings.BrowsersFileName);

        public PathService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
    }
}
