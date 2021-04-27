using Burls.Application.Browsers.Data;
using Burls.Domain;
using Burls.Persistence.Browsers.Factories;
using Burls.Persistence.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Data
{
    public class BrowserRepository : RepositoryBase<Browser, BurlsDbContext>, IBrowserRepository
    {
        public BrowserRepository(BurlsDbContext context)
            : base(context)
        {
        }

        public IEnumerable<InstalledBrowser> GetInstalledBrowsers()
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
