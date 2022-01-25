using Burls.Application.Browsers.State;
using Burls.Domain;
using Burls.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Burls.Application.Core.Services;
using Burls.Windows.ViewModels.Models;
using Burls.Windows.Services;
using System.Windows.Threading;
using System.Threading;
using Burls.Application.Browsers.Services;
using System.Collections.ObjectModel;
using Burls.Windows.Mappings;
using Burls.Application.Core.State;

namespace Burls.Windows.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IViewModel
    {
        private readonly IOperatingSystemService _operatingSystemService;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationState _applicationState;
        private readonly ISettingsService _settingsService;
        private readonly IBrowserService _browserService;
        private readonly IUpdateService _updateService;
        private readonly IBrowserState _browserState;

        public int ThemeIndex { get => (int)_applicationService.GetTheme(); set { _applicationService.SetTheme((ApplicationTheme)value); } }
        private string _versionDescription;
        public string VersionDescription { get => _versionDescription; private set { _versionDescription = value; OnPropertyChanged(); } }
        private VersionStatus _versionStatus;
        public VersionStatus VersionStatus { get => _versionStatus; private set { _versionStatus = value; OnPropertyChanged(); } }
        private string _latestVersion;
        public string LatestVersion { get => _latestVersion; private set { _latestVersion = value; OnPropertyChanged(); } }
        private LatestVersionStatus _latestVersionStatus;
        public LatestVersionStatus LatestVersionStatus { get => _latestVersionStatus; private set { _latestVersionStatus = value; OnPropertyChanged(); } }
        private bool _isDownloadingLatestVersion;
        public bool IsDownloadingLatestVersion { get => _isDownloadingLatestVersion; private set { _isDownloadingLatestVersion = value; OnPropertyChanged(); } }
        public bool AutoSyncBrowsersOnStartup { get => _applicationState.Settings.AutoSyncBrowsersOnStartup == true; set { _applicationState.Settings.AutoSyncBrowsersOnStartup = value; _settingsService.SaveSettings(_applicationState.Settings); } }
        public ObservableCollection<BrowserProfileViewModel> BrowserProfiles { get; set; }

        public ICommand OpenWindowsColorSettingsCommand { get; set; }
        public ICommand DownloadLatestVersionCommand { get; set; }
        public ICommand InstallLatestVersionCommand { get; set; }
        public ICommand SyncBrowserInfoCommand { get; set; }

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

            OpenWindowsColorSettingsCommand = new RelayCommand(_operatingSystemService.OpenColorSettings);
            DownloadLatestVersionCommand = new RelayCommand(async () => await DownloadLatestVersion());
            InstallLatestVersionCommand = new RelayCommand(async () => await _updateService.InstallLatestVersion());
            SyncBrowserInfoCommand = new RelayCommand(SyncBrowserInfo);

            BrowserProfiles = new ObservableCollection<BrowserProfileViewModel>();

            LoadBrowserProfiles();
        }

        private void SyncBrowserInfo()
        {
            _browserService.SyncBrowsers(true);
            _browserState.RefreshBrowserProfiles();
            LoadBrowserProfiles();
        }

        private void LoadBrowserProfiles()
        {
            BrowserProfiles.Clear();

            _browserState.BrowserProfiles.Select(x => new BrowserProfileViewModel(_browserService, x)).ToList().ForEach(x => BrowserProfiles.Add(x));
        }

        private async Task DownloadLatestVersion()
        {
            IsDownloadingLatestVersion = true;

            await _updateService.DownloadLatestVersion();

            IsDownloadingLatestVersion = false;

            LatestVersionStatus = await _updateService.GetLatestVersionStatus();
        }
    }
}
