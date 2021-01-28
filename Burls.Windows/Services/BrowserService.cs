using AutoMapper;
using Burls.Domain;
using Burls.Persistence;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrowserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task InitializeBrowsersAsync()
        {
            await _unitOfWork.BeginTransactionAsync();

            // Sync browsers
            var installedBrowsers = GetInstalledBrowsers();
            var browsers = await _unitOfWork.BrowserRepository.GetAllAsync();

            // Remove old browsers
            var browsersToRemove = browsers.Where(b => !installedBrowsers.Any(i => i.Name.Equals(b.Name, StringComparison.CurrentCultureIgnoreCase))).ToList();

            foreach (var browser in browsersToRemove)
            {
                await _unitOfWork.BrowserRepository.DeleteAsync(browser);
            }

            // Update existing browsers and profiles
            var browsersToUpdate = browsers.Where(b => installedBrowsers.Any(i => i.Name.Equals(b.Name, StringComparison.CurrentCultureIgnoreCase)));

            foreach (var browser in browsersToUpdate)
            {
                var installedBrowser = installedBrowsers.First(i => i.Name.Equals(browser.Name, StringComparison.CurrentCultureIgnoreCase));

                // Map browser
                _mapper.Map(installedBrowser, browser);

                // Sync profiles
                var installedProfiles = installedBrowser.Profiles;
                var profiles = browser.Profiles;

                // Remove old profiles
                var profilesToRemove = profiles.Where(p => !installedProfiles.Any(i => i.Name.Equals(p.Name, StringComparison.CurrentCultureIgnoreCase))).ToList();

                foreach (var profile in profilesToRemove)
                {
                    browser.Profiles.Remove(profile);
                }

                // Update existing browsers and profiles
                var profilesToUpdate = profiles.Where(p => installedProfiles.Any(i => i.Name.Equals(p.Name, StringComparison.CurrentCultureIgnoreCase)));

                foreach (var profile in profilesToUpdate)
                {
                    var installedProfile = installedProfiles.First(i => i.Name.Equals(profile.Name, StringComparison.CurrentCultureIgnoreCase));

                    // Map profile
                    _mapper.Map(installedProfile, profile);
                }

                // Create new profiles
                var profilesToCreate = installedProfiles.Where(i => !profiles.Any(p => p.Name.Equals(i.Name, StringComparison.CurrentCultureIgnoreCase)));

                foreach (var profile in profilesToCreate)
                {
                    var newProfile = _mapper.Map<Domain.Profile>(profile);

                    browser.Profiles.Add(newProfile);
                }
            }

            // Create new browsers
            var browsersToCreate = installedBrowsers.Where(i => !browsers.Any(b => b.Name.Equals(i.Name, StringComparison.CurrentCultureIgnoreCase)));

            foreach (var browser in browsersToCreate)
            {
                var newBrowser = _mapper.Map<Browser>(browser);

                await _unitOfWork.BrowserRepository.AddAsync(newBrowser);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }

        public async Task<IEnumerable<BrowserProfile>> GetBrowserProfilesAsync()
        {
            var browsers = await GetBrowsersAsync();
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

        private async Task<IEnumerable<Browser>> GetBrowsersAsync()
        {
            return await _unitOfWork.BrowserRepository.GetAllAsync();
        }

        private IEnumerable<InstalledBrowser> GetInstalledBrowsers()
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
            availableShortcuts.Add("0");

            return availableShortcuts;
        }

        public async Task UseBrowserProfileAsync(BrowserProfile browserProfile, string requestUrl, bool saveRequestUrl)
        {
            if (saveRequestUrl)
            {
                await _unitOfWork.BeginTransactionAsync();
                
                var profile = await _unitOfWork.ProfileRepository.GetAsync(browserProfile.Profile.Id);
                var requestUri = new Uri(requestUrl);

                profile.Websites.Add(new Website(requestUri.Host));

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }

            browserProfile.NavigateToUrl(requestUrl);

            Application.Current.Shutdown();
        }

        public async Task UseBrowserProfileIndexAsync(IEnumerable<BrowserProfile> browserProfiles, string index, string requestUrl, bool saveRequestUrl)
        {
            var browserProfile = browserProfiles.FirstOrDefault(x => x.Shortcut?.Equals(index) ?? false);

            if (browserProfile != null)
            {
                await UseBrowserProfileAsync(browserProfile, requestUrl, saveRequestUrl);
            }
        }
    }
}
