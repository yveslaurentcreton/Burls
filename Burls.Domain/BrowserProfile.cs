using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Domain
{
    public class BrowserProfile
    {
        public string Name { get; }
        public Browser Browser { get; }
        public Profile Profile { get; }
        public string IconPath { get; }
        public string Shortcut { get; }

        public BrowserProfile(Browser browser, Profile profile, string shortcut = null)
        {
            Name = $"{browser.Name} - {profile.DisplayName}";
            Browser = browser;
            Profile = profile;
            Shortcut = shortcut;
            IconPath = !string.IsNullOrEmpty(profile.IconPath) ? profile.IconPath : browser.IconPath;
        }

        public void NavigateToUrl(string url)
        {
            Browser.NavigateToUrl(url, Profile);
        }
    }
}
