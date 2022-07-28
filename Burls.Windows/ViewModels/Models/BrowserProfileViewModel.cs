using Burls.Application.Browsers.Services;
using Burls.Domain;
using Burls.Windows.Core;
using Burls.Windows.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Windows.ViewModels.Models
{
    public class BrowserProfileViewModel
    {
        private readonly BrowserProfile _browserProfile;

        public string Name => _browserProfile.Name;
        public string IconPath => _browserProfile.IconPath;
        public string Shortcut => _browserProfile.Shortcut;

        public BrowserViewModel Browser { get; }
        public ProfileViewModel Profile { get; }

        public BrowserProfileViewModel(BrowserProfile browserProfile)
        {
            _browserProfile = browserProfile;

            Browser = new BrowserViewModel(_browserProfile.Browser);
            Profile = new ProfileViewModel(_browserProfile.Profile);
        }
    }
}
