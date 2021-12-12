using Burls.Application.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly string _settingsFilePath;
        private bool _loaded = false;
        private IDictionary<string, string> _settings;

        public SettingsService()
        {
            _settingsFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Burls\\Configurations\\AppProperties.json";
        }

        public void LoadSettings()
        {
            if (!_loaded)
            {
                var json = File.ReadAllText(_settingsFilePath);
                _settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                _loaded = true;
            }
        }

        public void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(_settings);
            File.WriteAllText(_settingsFilePath, json);
        }

        public string GetSetting(string key)
        {
            LoadSettings();

            return _settings[key];
        }

        public void SetSetting(string key, string value)
        {
            _settings[key] = value;
            SaveSettings();
        }
    }
}
