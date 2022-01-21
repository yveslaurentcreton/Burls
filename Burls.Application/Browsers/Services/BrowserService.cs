using Burls.Application.Browsers.Data;
using Burls.Application.Core.Data;
using Burls.Application.Core.Services;
using Burls.Application.Core.State;
using Burls.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Burls.Application.Browsers.Services
{
    public class BrowserService : IBrowserService
    {
        private readonly IBrowserRepository _browserRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IApplicationState _applicationState;
        private readonly ISettingsService _settingsService;
        private readonly IApplicationService _applicationService;

        public BrowserService(
            IBrowserRepository browserRepository,
            AutoMapper.IMapper mapper,
            IApplicationState applicationState,
            ISettingsService settingsService,
            IApplicationService applicationService)
        {
            _browserRepository = browserRepository;
            _mapper = mapper;
            _applicationState = applicationState;
            _settingsService = settingsService;
            _applicationService = applicationService;
        }

        public IEnumerable<Browser> GetBrowsers()
        {
            return _browserRepository.GetBrowsers();
        }

        public IEnumerable<BrowserProfile> GetBrowserProfiles()
        {
            var browsers = GetBrowsers();
            var availableShortcuts = new Queue<string>(GetAvailableShortcuts());

            foreach (var browser in browsers)
            {
                foreach (var profile in browser.Profiles)
                {
                    string shortcut = availableShortcuts.Count > 0 ? availableShortcuts.Dequeue() : null;

                    yield return new BrowserProfile(browser, profile, shortcut);
                }
            }

            yield break;
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

        public void SyncBrowsers()
        {
            var settings = _applicationState.Settings;
            var autoSyncBrowsersOnStartup = settings.AutoSyncBrowsersOnStartup;

            if (!autoSyncBrowsersOnStartup == null)
            {
                autoSyncBrowsersOnStartup = true;
                settings.AutoSyncBrowsersOnStartup = false;
                _settingsService.SaveSettings(settings);
            }

            if (autoSyncBrowsersOnStartup == true)
            {
                // Sync browsers
                var installedBrowsers = _browserRepository.GetInstalledBrowsers();
                var browsers = _browserRepository.GetBrowsers();

                // Remove old browsers
                var browsersToRemove = browsers.Where(b => !installedBrowsers.Any(i => i.Name.Equals(b.Name, StringComparison.CurrentCultureIgnoreCase))).ToList();

                foreach (var browser in browsersToRemove)
                {
                    browsers.Remove(browser);
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

                    browsers.Add(newBrowser);
                }

                _browserRepository.SaveBrowsers();
            }
        }

        public async Task UseBrowserProfileAsync(BrowserProfile browserProfile, string requestUrl, bool saveRequestUrl)
        {
            if (saveRequestUrl)
            {
                var requestDomain = SelectionRule.GetPartFromUrl(SelectionRule.SelectionRuleParts.Domain, requestUrl);
                AddSelectionRule(
                    browserProfile.Profile,
                    SelectionRule.SelectionRuleParts.Domain,
                    SelectionRule.SelectionRuleCompareTypes.Equals,
                    requestDomain);
            }

            browserProfile.NavigateToUrl(requestUrl);

            _applicationService.Shutdown();
        }

        public SelectionRule AddSelectionRule(Profile profile, SelectionRule.SelectionRuleParts selectionRulePart, SelectionRule.SelectionRuleCompareTypes selectionRuleCompareType, string value)
        {
            var newSelectionRule = new SelectionRule(
                profile.Id,
                selectionRulePart,
                selectionRuleCompareType,
                value);
            profile.SelectionRules.Add(newSelectionRule);

            _browserRepository.SaveBrowsers();

            return newSelectionRule;
        }

        public void UpdateSelectionRule(SelectionRule selectionRule)
        {
            _browserRepository.SaveBrowsers();
        }

        public void DeleteSelectionRule(Profile profile, SelectionRule selectionRule)
        {
            profile.SelectionRules.Remove(selectionRule);

            _browserRepository.SaveBrowsers();
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
