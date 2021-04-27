using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Factories
{
    public abstract class ChromiumFactory : BrowserFactory
    {
        public ChromiumFactory(MintPlayer.PlatformBrowser.Browser browserInfo)
            : base(browserInfo)
        {
        }
    }
}
