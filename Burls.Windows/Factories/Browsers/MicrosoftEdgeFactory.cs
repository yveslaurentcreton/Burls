using Burls.Windows.Factories.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Factories.Browsers
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
