using Burls.Windows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class BrowserService : IBrowserService
    {
        public IReadOnlyList<Browser> GetBrowsers()
        {
            var installedBrowsers = MintPlayer.PlatformBrowser.PlatformBrowser.GetInstalledBrowsers();

            return installedBrowsers.Select(x =>
            {
                return new Browser(
                    x.Name,
                    x.ExecutablePath,
                    x.IconPath,
                    x.IconIndex,
                    x.Version,
                    x.FileAssociations,
                    x.UrlAssociations);
            }).ToList();
        }
    }
}
