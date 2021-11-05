using Burls.Persistence.Profiles.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Factories
{
    public class MicrosoftEdgeFactory : BrowserFactory
    {
        public const string FILENAME = "MicrosoftEdge.exe";

        public MicrosoftEdgeFactory(MintPlayer.PlatformBrowser.Browser browserInfo)
            : base(browserInfo)
        {

        }

        protected override ProfileFactory GetProfileFactory()
        {
            return new MicrosoftEdgeProfileFactory();
        }
    }
}
