using Burls.Application.Browsers.Data;
using Burls.Application.Browsers.Services;
using Burls.Application.Core.Services;
using Burls.Core.Data;
using Burls.Domain;
using Burls.Persistence.Browsers.Factories;
using Burls.Persistence.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Browsers.Data
{
    public class BrowserRepository : IBrowserRepository
    {
        private readonly IPathService _pathService;
        private readonly Lazy<IEnumerable<Browser>> _lazyBrowsers;

        public BrowserRepository(IPathService pathService)
        {
            _lazyBrowsers = new Lazy<IEnumerable<Browser>>(() =>
            {
                var json = File.ReadAllText(_pathService.BrowsersFileName);
                var browsers = JsonConvert.DeserializeObject<IEnumerable<Browser>>(json).ToList();

                return browsers;
            });
            _pathService = pathService;
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

        public IEnumerable<Browser> GetBrowsers()
        {
            return _lazyBrowsers.Value;
        }

        public void SaveBrowsers()
        {
            var browsers = _lazyBrowsers.Value;
            var json = JsonConvert.SerializeObject(browsers);
            File.WriteAllText(_pathService.BrowsersFileName, json);
        }
    }
}
