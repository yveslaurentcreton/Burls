using Burls.Application.Core.Data;
using Burls.Application.Core.Services;
using Burls.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Core.Data
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IPathService _pathService;

        public SettingsRepository(IPathService pathService)
        {
            _pathService = pathService;
        }

        public Settings GetSettings()
        {
            Settings settings = null;

            if (File.Exists(_pathService.SettingsFileName))
            {
                var json = File.ReadAllText(_pathService.SettingsFileName);
                settings = JsonConvert.DeserializeObject<Settings>(json);
            }
            else
            {
                settings = new Settings() { Theme = ApplicationTheme.OsDefault.ToString(), AutoSyncBrowsersOnStartup = null};
            }

            return settings;
        }

        public void SaveSettings(Settings settings)
{
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_pathService.SettingsFileName, json);
        }
    }
}
