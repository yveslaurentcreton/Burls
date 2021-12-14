using Burls.Application.Core.Services;
using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.State
{
    public class ApplicationState : StateBase, IApplicationState
    {
        private readonly Lazy<Settings> _lazySettings;

        public string[] StartUpArgs { get; set; }
        public ApplicationMode ApplicationMode { get; set; }
        public Settings Settings { get => _lazySettings.Value; }

        public ApplicationState(ISettingsService settingsService)
        {
            _lazySettings = new Lazy<Settings>(settingsService.GetSettings);
        }
    }

    public enum ApplicationMode
    {
        Select,
        Settings
    }
}
