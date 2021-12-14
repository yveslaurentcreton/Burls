﻿using Burls.Application.Browsers.State;
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

namespace Burls.Windows.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IViewModel
    {
        private readonly IOperatingSystemService _operatingSystemService;
        private readonly IApplicationService _applicationService;
        private readonly IUpdateService _updateService;

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
        public IEnumerable<BrowserProfileViewModel> BrowserProfiles { get; set; }

        public ICommand OpenWindowsColorSettingsCommand { get; set; }
        public ICommand DownloadLatestVersionCommand { get; set; }
        public ICommand InstallLatestVersionCommand { get; set; }

        public SettingsViewModel(
            IOperatingSystemService operatingSystemService,
            IApplicationService applicationService,
            IBrowserService browserService,
            IUpdateService updateService,
            IBrowserState browserState)
        {
            _operatingSystemService = operatingSystemService;
            _applicationService = applicationService;
            _updateService = updateService;

            VersionDescription = $"v{_applicationService.GetVersion()}";
            _updateService.GetVersionStatus().ContinueWith(versionStatusTask => VersionStatus = versionStatusTask.Result, TaskScheduler.FromCurrentSynchronizationContext());
            _updateService.GetLatestVersionInfo().ContinueWith(versionTask => LatestVersion = $"v{versionTask.Result.Version}", TaskScheduler.FromCurrentSynchronizationContext());
            _updateService.GetLatestVersionStatus().ContinueWith(latestVersionStatusTask => LatestVersionStatus = latestVersionStatusTask.Result, TaskScheduler.FromCurrentSynchronizationContext());
            BrowserProfiles = browserState.BrowserProfiles.Select(x => new BrowserProfileViewModel(browserService, x)).ToList();

            OpenWindowsColorSettingsCommand = new RelayCommand(_operatingSystemService.OpenColorSettings);
            DownloadLatestVersionCommand = new RelayCommand(async () => await DownloadLatestVersion());
            InstallLatestVersionCommand = new RelayCommand(async () => await _updateService.InstallLatestVersion());
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
