using Burls.Application.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ISettingsService _settingsService;

        public void AddProperty(object key, object value) => System.Windows.Application.Current.Properties.Add(key, value);

        public IDictionary<string, object> GetProperties() => null;

        public ApplicationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void Shutdown()
        {
            Microsoft.UI.Xaml.Application.Current.Exit();
        }

        public Version GetVersion()
        {
            // Set the app version in Burls.Windows > Properties > Package > PackageVersion
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            return new Version(version);
        }

        public ApplicationTheme GetTheme()
        {
            var themeName = _settingsService.GetSetting("Theme");

            if (Enum.TryParse<ApplicationTheme>(themeName, out var theme))
            {
                return theme;
            }
            else
            {
                return ApplicationTheme.OsDefault;
            }
        }

        public void SetTheme(ApplicationTheme theme)
        {
            _settingsService.SetSetting("Theme", theme.ToString());
        }
    }
}
