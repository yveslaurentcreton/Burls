using Burls.Application.Core.Data;
using Burls.Application.Core.Services;
using Burls.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public Settings GetSettings()
        {
            return _settingsRepository.GetSettings();
        }

        public void SaveSettings(Settings settings)
        {
            _settingsRepository.SaveSettings(settings);
        }
    }
}
