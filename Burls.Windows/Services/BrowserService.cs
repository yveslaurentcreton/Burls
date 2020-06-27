using Burls.Windows.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class BrowserService : IBrowserService
    {
        private readonly IProfileService _profileService;

        public BrowserService(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public IReadOnlyList<Browser> GetBrowsers()
        {
            var installedBrowsers = MintPlayer.PlatformBrowser.PlatformBrowser.GetInstalledBrowsers().ToList();

            // Filter browsers
            if (installedBrowsers.Any(x => (new FileInfo(x.Version.FileName)).Name.Equals(Browser.NEWEDGEFILENAME, StringComparison.OrdinalIgnoreCase)))
            {
                installedBrowsers = installedBrowsers
                    .Where(x => !(new FileInfo(x.Version.FileName)).Name.Equals(Browser.OLDEDGEFILENAME, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return installedBrowsers.Select(x =>
            {
                var fileInfo = new FileInfo(x.Version.FileName);
                var profiles = _profileService.GetProfiles(fileInfo.Name);

                // Determine specific properties for specific browsers
                string profileArgumentName;

                switch ((new FileInfo(x.Version.FileName)).Name)
                {
                    case Browser.CHROMEFILENAME:
                    case Browser.NEWEDGEFILENAME:
                        profileArgumentName = "--profile-directory";
                        break;
                    default:
                        profileArgumentName = null;
                        break;
                }

                return new Browser(x.Name,
                    x.ExecutablePath,
                    x.IconPath,
                    x.IconIndex,
                    x.Version,
                    x.FileAssociations,
                    x.UrlAssociations,
                    profileArgumentName,
                    profiles);
            }).ToList();
        }
    }
}
