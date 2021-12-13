using Burls.Application.Browsers.State;
using Burls.Application.Profiles.Commands;
using Burls.Domain;
using Burls.Windows.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Burls.Application.Core.Services;
using static Burls.Domain.SelectionRule;
using Burls.Windows.ViewModels.Models;
using Burls.Windows.Services;
using Burls.Application.Core.Queries;
using System.Windows.Threading;
using System.Threading;

namespace Burls.Windows.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IViewModel
    {
        private readonly IOperatingSystemService _operatingSystemService;
        private readonly IApplicationService _applicationService;
        private readonly IUpdateService _updateService;
        private readonly IMediator _mediator;

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
            IUpdateService updateService,
            IBrowserState browserState,
            IBrowserStateNotificationService browserStateNotificationService,
            IMediator mediator)
        {
            _operatingSystemService = operatingSystemService;
            _applicationService = applicationService;
            _updateService = updateService;
            _mediator = mediator;

            var version = _mediator.Send(new GetApplicationVersionQuery()).Result.Version;
            VersionDescription = $"v{version}";
            _mediator.Send(new GetApplicationVersionQuery()).ContinueWith(responseTask => VersionDescription = $"v{responseTask.Result.Version}");
            _updateService.GetVersionStatus().ContinueWith(versionStatusTask => VersionStatus = versionStatusTask.Result, TaskScheduler.FromCurrentSynchronizationContext());
            _updateService.GetLatestVersionInfo().ContinueWith(versionTask => LatestVersion = $"v{versionTask.Result.Version}", TaskScheduler.FromCurrentSynchronizationContext());
            _updateService.GetLatestVersionStatus().ContinueWith(latestVersionStatusTask => LatestVersionStatus = latestVersionStatusTask.Result, TaskScheduler.FromCurrentSynchronizationContext());
            BrowserProfiles = browserState.BrowserProfiles.Select(x => new BrowserProfileViewModel(browserStateNotificationService, mediator, x)).ToList();

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
