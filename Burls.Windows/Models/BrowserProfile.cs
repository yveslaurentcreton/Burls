using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Windows.Models
{
    public class BrowserProfile
    {
        public string Name { get; }
        public Browser Browser { get; }
        public Profile Profile { get; }
        public ImageSource IconImageSource { get; }
        public string Shortcut { get; }

        public BrowserProfile(Browser browser, Profile profile, string shortcut = null)
        {
            Name = $"{browser.Name} - {profile.Name}";
            Browser = browser;
            Profile = profile;
            Shortcut = shortcut;
            IconImageSource = profile.IconImageSource ?? browser.IconImageSource;
        }

        public void NavigateToUrl(string url)
        {
            Browser.NavigateToUrl(url, Profile);
        }
    }
}
