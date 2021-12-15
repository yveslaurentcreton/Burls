using Burls.Application.Core.Services;
using Burls.Application.Core.State;
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
        private readonly IApplicationState _applicationState;
        private readonly ISettingsService _settingsService;

        public ApplicationService(IApplicationState applicationState, ISettingsService settingsService)
        {
            _applicationState = applicationState;
            _settingsService = settingsService;
        }

        public void Shutdown()
        {
            Environment.Exit(0);
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
            var settings = _applicationState.Settings;
            var themeName = settings.Theme;

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
            _applicationState.Settings.Theme = theme.ToString();
            _settingsService.SaveSettings(_applicationState.Settings);
        }
    }
}
