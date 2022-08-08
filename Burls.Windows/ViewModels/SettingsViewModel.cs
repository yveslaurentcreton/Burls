using Burls.Application.Browsers.State;
using Burls.Domain;
using Burls.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burls.Application.Core.Services;
using Burls.Windows.ViewModels.Models;
using Burls.Windows.Services;
using System.Windows.Threading;
using System.Threading;
using Burls.Application.Browsers.Services;
using System.Collections.ObjectModel;
using Burls.Windows.Mappings;
using Burls.Application.Core.State;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Burls.Windows.ViewModels.Models.Messages;

namespace Burls.Windows.ViewModels
{
    public partial class SettingsViewModel : ObservableRecipient, IViewModel
    {
        private readonly IOperatingSystemService _operatingSystemService;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationState _applicationState;
        private readonly ISettingsService _settingsService;
        private readonly IBrowserService _browserService;
        private readonly IUpdateService _updateService;
        private readonly IBrowserState _browserState;

        public int ThemeIndex { get => (int)_applicationService.GetTheme(); set { _applicationService.SetTheme((ApplicationTheme)value); } }
        [ObservableProperty]
        private string _versionDescription;
        [ObservableProperty]
        private VersionStatus _versionStatus;
        [ObservableProperty]
        private string _latestVersion;
        [ObservableProperty]
        private LatestVersionStatus _latestVersionStatus;
        [ObservableProperty]
        private bool _isDownloadingLatestVersion;
        public bool AutoSyncBrowsersOnStarup { get => _applicationState.Settings.AutoSyncBrowsersOnStartup == true; set { _applicationState.Settings.AutoSyncBrowsersOnStartup = value; _settingsService.SaveSettings(_applicationState.Settings); } }
        public ObservableCollection<BrowserProfileViewModel> BrowserProfiles { get; set; }

        public SettingsViewModel(
            IOperatingSystemService operatingSystemService,
            IApplicationService applicationService,
            IApplicationState applicationState,
            ISettingsService settingsService,
            IBrowserService browserService,
            IUpdateService updateService,
            IBrowserState browserState)
        {
            _operatingSystemService = operatingSystemService;
            _applicationService = applicationService;
            _applicationState = applicationState;
            _settingsService = settingsService;
            _browserService = browserService;
            _updateService = updateService;
            _browserState = browserState;

            VersionDescription = $"v{_applicationService.GetVersion()}";
            _updateService.GetVersionStatus().ContinueWith(versionStatusTask => VersionStatus = versionStatusTask.Result, TaskScheduler.FromCurrentSynchronizationContext());
            _updateService.GetLatestVersionInfo().ContinueWith(versionTask => LatestVersion = $"v{versionTask.Result.Version}", TaskScheduler.FromCurrentSynchronizationContext());
            _updateService.GetLatestVersionStatus().ContinueWith(latestVersionStatusTask => LatestVersionStatus = latestVersionStatusTask.Result, TaskScheduler.FromCurrentSynchronizationContext());

            BrowserProfiles = new ObservableCollection<BrowserProfileViewModel>();

            LoadBrowserProfiles();

            IsActive = true;
        }

        [RelayCommand]
        private void SyncBrowserInfo()
        {
            _browserService.SyncBrowsers(true);
            _browserState.RefreshBrowserProfiles();
            LoadBrowserProfiles();
        }

        private void LoadBrowserProfiles()
        {
            BrowserProfiles.Clear();

            _browserState.BrowserProfiles.Select(x => new BrowserProfileViewModel(x)).ToList().ForEach(x => BrowserProfiles.Add(x));
        }

        [RelayCommand]
        private void OpenWindowsColorSettings() => _operatingSystemService.OpenColorSettings();

        [RelayCommand]
        private async Task DownloadLatestVersion()
        {
            IsDownloadingLatestVersion = true;

            await _updateService.DownloadLatestVersion();

            IsDownloadingLatestVersion = false;

            LatestVersionStatus = await _updateService.GetLatestVersionStatus();
        }

        [RelayCommand]
        private async Task InstallLatestVersion() => await _updateService.InstallLatestVersion();
    }        
}
