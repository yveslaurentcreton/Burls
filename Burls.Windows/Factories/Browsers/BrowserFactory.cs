using Burls.Domain;
using Burls.Windows.Factories.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Factories.Browsers
{
    public class BrowserFactory
    {
        private readonly MintPlayer.PlatformBrowser.Browser _browserInfo;

        public BrowserFactory(MintPlayer.PlatformBrowser.Browser browserInfo)
        {
            _browserInfo = browserInfo;
        }

        public Browser GetBrowser()
        {
            return new Browser(
                _browserInfo.Name,
                _browserInfo.ExecutablePath,
                _browserInfo.IconPath,
                _browserInfo.IconIndex,
                GetProfileArgumentName(),
                GetProfiles());
        }

        protected virtual string GetProfileArgumentName()
        {
            return null;
        }

        protected virtual IReadOnlyList<Profile> GetProfiles()
        {
            return GetProfileFactory()
                .GetProfiles()
                .OrderBy(x => x.Name)
                .ToList();
        }

        protected virtual ProfileFactory GetProfileFactory()
        {
            return new ProfileFactory();
        }
    }
}
