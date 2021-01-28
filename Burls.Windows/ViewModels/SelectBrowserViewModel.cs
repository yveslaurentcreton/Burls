using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Burls.Windows.Constants;
using Burls.Windows.Models;
using Burls.Windows.Services;
using Burls.Windows.State;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Burls.Windows.ViewModels
{
    public class SelectBrowserViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IRegionNavigationService _navigationService;
        private readonly IBrowserService _browserService;

        public IBrowserStore BrowserStore { get; }
        public ICommand UseBrowserProfileCommand { get; }

        public SelectBrowserViewModel(IRegionManager regionManager, IBrowserService browserService, IBrowserStore browserStore)
        {
            _regionManager = regionManager;
            _navigationService = regionManager.Regions[Regions.Main].NavigationService;
            _browserService = browserService;
            BrowserStore = browserStore;

            BrowserStore.RequestUrl = (Application.Current as App).RequestUrl;
            BrowserStore.BrowserProfiles = _browserService.GetBrowserProfilesAsync().Result;

            UseBrowserProfileCommand = new DelegateCommand<BrowserProfile>(async (browserProfile) => await UseBrowserProfile(browserProfile));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        

        private Task UseBrowserProfile(BrowserProfile browserProfile)
        {
            return _browserService.UseBrowserProfileAsync(browserProfile, BrowserStore.RequestUrl, BrowserStore.SaveRequestUrl);
        }
    }
}
