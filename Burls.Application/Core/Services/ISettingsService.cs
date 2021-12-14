using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public interface ISettingsService
    {
        //void LoadSettings();
        //void SaveSettings();
        //string GetSetting(string key);
        //void SetSetting(string key, string value);

        // New
        Settings GetSettings();
        void SaveSettings(Settings settings);
    }
}
