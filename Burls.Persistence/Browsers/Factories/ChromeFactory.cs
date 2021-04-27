using Burls.Persistence.Profiles.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Factories
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

        protected override string GetProfileArgumentName()
        {
            return PROFILEARGUMENTNAME;
        }
    }
}
