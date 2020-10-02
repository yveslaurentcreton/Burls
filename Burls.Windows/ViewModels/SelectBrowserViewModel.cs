using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using Burls.Windows.Constants;
using Burls.Windows.Core.Contracts.Services;
using Burls.Windows.Models;
using Burls.Windows.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Burls.Windows.ViewModels
{
    public class SelectBrowserViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationService _navigationService;
        private readonly IBrowserService _browserService;

        public string RequestUrl { get; set; }

        private IReadOnlyList<BrowserProfile> _browserProfiles;
        public IReadOnlyList<BrowserProfile> BrowserProfiles
        {
            get { return _browserProfiles; }
            set
            {
                _browserProfiles = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ApplicationShutdownCommand { get; set; }
        public ICommand UseBrowserProfileCommand { get; set; }
        public ICommand UseBrowserProfileIndexCommand { get; set; }

        public SelectBrowserViewModel(IRegionManager regionManager, IBrowserService browserService)
        {
            _regionManager = regionManager;
            _navigationService = regionManager.Regions[Regions.Main].NavigationService;
            _browserService = browserService;

            RequestUrl = (Application.Current as App).RequestUrl;
            BrowserProfiles = GetBrowserProfiles();

            ApplicationShutdownCommand = new DelegateCommand(ApplicationShutdown);
            UseBrowserProfileCommand = new DelegateCommand<BrowserProfile>(UseBrowserProfile);
            UseBrowserProfileIndexCommand = new DelegateCommand<string>(UseBrowserProfileIndex);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        private IReadOnlyList<string> GetAvailableShortcuts()
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

        private IReadOnlyList<BrowserProfile> GetBrowserProfiles()
        {
            var browsers = _browserService.GetBrowsers();
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

        private void ApplicationShutdown()
        {
            Application.Current.Shutdown();
        }

        private void UseBrowserProfileIndex(string browserProfileIndex)
        {
            var browserProfile = BrowserProfiles.FirstOrDefault(x => x.Shortcut?.Equals(browserProfileIndex) ?? false);

            if (browserProfile != null)
            {
                UseBrowserProfileCommand.Execute(browserProfile);
            }
        }

        private void UseBrowserProfile(BrowserProfile browserProfile)
        {
            browserProfile.NavigateToUrl(RequestUrl);

            ApplicationShutdownCommand.Execute(null);
        }
    }
}
