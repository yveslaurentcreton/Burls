using Burls.Persistence.Profiles.Factories;
using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Factories
{
    public class BrowserFactory
    {
        private readonly MintPlayer.PlatformBrowser.Browser _browserInfo;

        public BrowserFactory(MintPlayer.PlatformBrowser.Browser browserInfo)
        {
            _browserInfo = browserInfo;
        }

        public InstalledBrowser GetBrowser()
        {
            return new InstalledBrowser(
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

        protected virtual IReadOnlyList<InstalledProfile> GetProfiles()
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
