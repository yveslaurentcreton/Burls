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
        private readonly Lazy<ICollection<Browser>> _lazyBrowsers;

        public BrowserRepository(IPathService pathService)
        {
            _lazyBrowsers = new Lazy<ICollection<Browser>>(() =>
            {
                List<Browser> browsers = null;

                if (File.Exists(_pathService.BrowsersFileName))
                {
                    var json = File.ReadAllText(_pathService.BrowsersFileName);
                    browsers = JsonConvert.DeserializeObject<ICollection<Browser>>(json).ToList();
                }

                return browsers ?? new List<Browser>();
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

        public ICollection<Browser> GetBrowsers()
        {
            return _lazyBrowsers.Value;
        }

        public void SaveBrowsers()
        {
            var browsers = GetBrowsers();
            var json = JsonConvert.SerializeObject(browsers);
            var fileName = _pathService.BrowsersFileName;
            var directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllText(fileName, json);
        }
    }
}
