using Burls.Windows.Factories;
using Burls.Windows.Factories.Browsers;
using Burls.Windows.Models;
using Burls.Windows.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Burls.Windows.Services
{
    public class BrowserService : IBrowserService
    {
        public BrowserService()
        {
        }

        public IEnumerable<Browser> GetBrowsers()
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

        public IEnumerable<BrowserProfile> GetBrowserProfiles()
        {
            var browsers = GetBrowsers();
            var browserProfiles = new List<BrowserProfile>();
            var availableShortcuts = new Queue<string>(GetAvailableShortcuts());

            foreach (var browser in browsers)
            {
                foreach (var profile in browser.Profiles)
                {
                    string shortcut = availableShortcuts.Count > 0 ? availableShortcuts.Dequeue() : null;

                    browserProfiles.Add(new BrowserProfile(browser, profile, shortcut));
                }
            }

            return browserProfiles;
        }

        private IEnumerable<string> GetAvailableShortcuts()
        {
            var availableShortcuts = new List<string>();

            availableShortcuts.Add("1");
            availableShortcuts.Add("2");
            availableShortcuts.Add("3");
            availableShortcuts.Add("4");
            availableShortcuts.Add("5");
            availableShortcuts.Add("6");
            availableShortcuts.Add("7");
            availableShortcuts.Add("8");
            availableShortcuts.Add("9");

            return availableShortcuts;
        }

        public void UseBrowserProfile(BrowserProfile browserProfile, string requestUrl)
        {
            browserProfile.NavigateToUrl(requestUrl);

            Application.Current.Shutdown();
        }

        public void UseBrowserProfileIndex(IEnumerable<BrowserProfile> browserProfiles, string index, string requestUrl)
        {
            var browserProfile = browserProfiles.FirstOrDefault(x => x.Shortcut?.Equals(index) ?? false);

            if (browserProfile != null)
            {
                UseBrowserProfile(browserProfile, requestUrl);
            }
        }
    }
}
