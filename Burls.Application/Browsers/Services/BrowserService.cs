using AutoMapper;
using Burls.Application.Core.Commands;
using Burls.Application.Core.Data;
using Burls.Application.Core.Services;
using Burls.Application.Profiles.Commands;
using Burls.Domain;
using MediatR;
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
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrowserService(
            IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task InitializeBrowsersAsync()
        {
            await _unitOfWork.BeginTransactionAsync();

            // Sync browsers
            var installedBrowsers = _unitOfWork.BrowserRepository.GetInstalledBrowsers();
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
                var requestDomain = SelectionRule.GetPartFromUrl(SelectionRule.SelectionRuleParts.Domain, requestUrl);
                var command = new CreateProfileSelectionRuleCommand(
                    browserProfile.Profile.Id,
                    SelectionRule.SelectionRuleParts.Domain,
                    SelectionRule.SelectionRuleCompareTypes.Equals,
                    requestDomain);

                await _mediator.Send(command);
            }

            browserProfile.NavigateToUrl(requestUrl);

            await _mediator.Send(new ApplicationShutdownCommand());
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
