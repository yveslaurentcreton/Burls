using Burls.Windows.Factories.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Factories.Browsers
{
    public abstract class ChromiumFactory : BrowserFactory
    {
        public ChromiumFactory(MintPlayer.PlatformBrowser.Browser browserInfo)
            : base(browserInfo)
        {

        }
    }
}
