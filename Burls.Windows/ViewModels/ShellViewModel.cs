using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using Burls.Windows.Constants;
using Burls.Windows.Properties;
using Burls.Windows.Services;
using Burls.Windows.State;
using MahApps.Metro.Controls;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Burls.Windows.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IBrowserService _browserService;
        private readonly IBrowserStore _browserStore;
        private IRegionNavigationService _navigationService;
        private HamburgerMenuItem _selectedMenuItem;
        private HamburgerMenuItem _selectedOptionsMenuItem;
        private DelegateCommand _goBackCommand;

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        public HamburgerMenuItem SelectedOptionsMenuItem
        {
            get { return _selectedOptionsMenuItem; }
            set { SetProperty(ref _selectedOptionsMenuItem, value); }
        }

        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellSelectBrowserPage, Glyph = "\uE774", Tag = PageKeys.SelectBrowser },
        };

        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellSettingsPage, Glyph = "\uE713", Tag = PageKeys.Settings }
        };

        public DelegateCommand GoBackCommand => _goBackCommand ??= new DelegateCommand(OnGoBack, CanGoBack);

        public ICommand LoadedCommand { get; }
        public ICommand UnloadedCommand { get; }
        public ICommand MenuItemInvokedCommand { get; }
        public ICommand OptionsMenuItemInvokedCommand { get; }
        public ICommand ApplicationShutdownCommand { get; }
        public ICommand UseBrowserProfileIndexCommand { get; }

        public ShellViewModel(IRegionManager regionManager, IBrowserService browserService, IBrowserStore browserStore)
        {
            _regionManager = regionManager;
            _browserService = browserService;
            _browserStore = browserStore;

            LoadedCommand = new DelegateCommand(OnLoaded);
            UnloadedCommand = new DelegateCommand(OnUnloaded);
            MenuItemInvokedCommand = new DelegateCommand(OnMenuItemInvoked);
            OptionsMenuItemInvokedCommand = new DelegateCommand(OnOptionsMenuItemInvoked);
            ApplicationShutdownCommand = new DelegateCommand(ApplicationShutdown);
            UseBrowserProfileIndexCommand = new DelegateCommand<string>(UseBrowserProfileIndex);
        }

        private void OnLoaded()
        {
            _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
            _navigationService.Navigated += OnNavigated;
            SelectedMenuItem = MenuItems.First();
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
            _regionManager.Regions.Remove(Regions.Main);
        }

        private bool CanGoBack()
            => _navigationService != null && _navigationService.Journal.CanGoBack;

        private void OnGoBack()
            => _navigationService.Journal.GoBack();

        private void OnMenuItemInvoked()
            => RequestNavigate(SelectedMenuItem.Tag?.ToString());

        private void OnOptionsMenuItemInvoked()
            => RequestNavigate(SelectedOptionsMenuItem.Tag?.ToString());

        private void RequestNavigate(string target)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target);
            }
        }

        private void OnNavigated(object sender, RegionNavigationEventArgs e)
        {
            var item = MenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => e.Uri.ToString() == i.Tag?.ToString());
            if (item != null)
            {
                SelectedMenuItem = item;
            }
            else
            {
                SelectedOptionsMenuItem = OptionMenuItems
                        .OfType<HamburgerMenuItem>()
                        .FirstOrDefault(i => e.Uri.ToString() == i.Tag?.ToString());
            }

            GoBackCommand.RaiseCanExecuteChanged();
        }

        private void ApplicationShutdown()
        {
            Application.Current.Shutdown();
        }

        private void UseBrowserProfileIndex(string browserProfileIndex)
        {
            _browserService.UseBrowserProfileIndex(_browserStore.BrowserProfiles, browserProfileIndex, _browserStore.RequestUrl);
        }
    }
}
