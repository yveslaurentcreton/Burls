using Burls.Windows.Factories;
using Burls.Windows.Factories.Browsers;
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
        public BrowserService()
        {
        }

        public IReadOnlyList<Browser> GetBrowsers()
        {
            var installedBrowsers = MintPlayer.PlatformBrowser.PlatformBrowser.GetInstalledBrowsers().ToList();

            // Filter browsers
            if (installedBrowsers.Any(x => (new FileInfo(x.Version.FileName)).Name.Equals(ChromiumEdgeFactory.FILENAME, StringComparison.OrdinalIgnoreCase)))
            {
                installedBrowsers = installedBrowsers
                    .Where(x => !(new FileInfo(x.Version.FileName)).Name.Equals(MicrosoftEdgeFactory.FILENAME, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return installedBrowsers.Select(x =>
            {
                var fileInfo = new FileInfo(x.Version.FileName);

                // Determine specific properties for specific browsers
                BrowserFactory browserFactory;

                switch ((new FileInfo(x.Version.FileName)).Name)
                {
                    case ChromeFactory.FILENAME:
                        browserFactory = new ChromeFactory(x);
                        break;
                    case ChromiumEdgeFactory.FILENAME:
                        browserFactory = new ChromiumEdgeFactory(x);
                        break;
                    default:
                        browserFactory = new BrowserFactory(x);
                        break;
                }

                return browserFactory.GetBrowser();
            }).ToList();
        }
    }
}
