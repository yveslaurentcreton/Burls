using Burls.Windows.Factories.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Factories.Browsers
{
    public class ChromeFactory : ChromiumFactory
    {
        public const string FILENAME = "chrome.exe";
        public const string PROFILEARGUMENTNAME = "--profile-directory";

        public ChromeFactory(MintPlayer.PlatformBrowser.Browser browserInfo)
            : base(browserInfo)
        {

        }

        protected override ProfileFactory GetProfileFactory()
        {
            return new ChromeProfileFactory();
        }
    }
}
