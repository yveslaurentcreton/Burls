using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Models
{
    public class BrowserProfile
    {
        public string Name { get; }
        public Browser Browser { get; }
        public Profile Profile { get; }

        public BrowserProfile(Browser browser, Profile profile)
        {
            Name = $"{browser.Name} - {profile.Name}";
            Browser = browser;
            Profile = profile;
        }

        public void NavigateToUrl(string url)
        {
            Browser.NavigateToUrl(url, Profile);
        }
    }
}
