using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Burls.Application.Core.State;
using Burls.Windows.Constants;
using Burls.Windows.Events;
using Burls.Windows.Properties;
using Burls.Windows.Services;
using MahApps.Metro.Controls;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Burls.Windows.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IApplicationState _applicationStore;
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

        private ObservableCollection<HamburgerMenuItem> _menuItems;
        public ObservableCollection<HamburgerMenuItem> MenuItems { get { return _menuItems ??= GetMenuItems(); } }

        private ObservableCollection<HamburgerMenuItem> _optionMenuItems;
        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get { return _optionMenuItems ??= GetOptionMenuItems(); } }

        public DelegateCommand GoBackCommand => _goBackCommand ??= new DelegateCommand(OnGoBack, CanGoBack);

        public ICommand LoadedCommand { get; }
        public ICommand UnloadedCommand { get; }
        public ICommand MenuItemInvokedCommand { get; }
        public ICommand OptionsMenuItemInvokedCommand { get; }
        public ICommand CommandTriggerEventCommand { get; }

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IApplicationState applicationStore, IFlyoutService flyoutService)
        {
            _regionManager = regionManager;
            _applicationStore = applicationStore;

            LoadedCommand = new DelegateCommand(OnLoaded);
            UnloadedCommand = new DelegateCommand(OnUnloaded);
            MenuItemInvokedCommand = new DelegateCommand(OnMenuItemInvoked);
            OptionsMenuItemInvokedCommand = new DelegateCommand(OnOptionsMenuItemInvoked);
            CommandTriggerEventCommand = new DelegateCommand<string>((commandParameter) => eventAggregator.GetEvent<CommandTriggerEvent>().Publish(new CommandTriggerEventArgs(commandParameter)));
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

        private ObservableCollection<HamburgerMenuItem> GetMenuItems()
        {
            var menuItems = new ObservableCollection<HamburgerMenuItem>();

            if (_applicationStore.ApplicationMode == ApplicationMode.Select)
            {
                menuItems.Add(new HamburgerMenuGlyphItem() { Label = Resources.ShellSelectBrowserPage, Glyph = "\uE774", Tag = PageKeys.BrowserProfileSelection });
            }

            menuItems.Add(new HamburgerMenuGlyphItem() { Label = Resources.ShellBrowserProfileSetupPage, Glyph = "\uEB41", Tag = PageKeys.BrowserProfileSetup });

            return menuItems;
        }

        private ObservableCollection<HamburgerMenuItem> GetOptionMenuItems()
        {
            return new ObservableCollection<HamburgerMenuItem>()
            {
                new HamburgerMenuGlyphItem() { Label = Resources.ShellSettingsPage, Glyph = "\uE713", Tag = PageKeys.Settings }
            };
        }
    }
}
