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

namespace Burls.Windows.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IViewModel
    {
        private readonly IOperatingSystemService _operatingSystemService;
        private readonly IApplicationService _applicationService;
        private readonly IMediator _mediator;

        public int ThemeIndex { get => (int)_applicationService.GetTheme() ; set { _applicationService.SetTheme((ApplicationTheme)value); } }
        public string VersionDescription { get; }
        public IEnumerable<BrowserProfileViewModel> BrowserProfiles { get; set; }

        public ICommand OpenWindowsColorSettingsCommand { get; set; }

        public SettingsViewModel(
            IOperatingSystemService operatingSystemService,
            IApplicationService applicationService,
            IBrowserState browserState,
            IBrowserStateNotificationService browserStateNotificationService,
            IMediator mediator)
        {
            _operatingSystemService = operatingSystemService;
            _applicationService = applicationService;
            _mediator = mediator;

            var version = _mediator.Send(new GetApplicationVersionQuery()).Result.Version;
            VersionDescription = $"Burls - {version}";
            BrowserProfiles = browserState.BrowserProfiles.Select(x => new BrowserProfileViewModel(browserStateNotificationService, mediator, x)).ToList();

            OpenWindowsColorSettingsCommand = new RelayCommand(_operatingSystemService.OpenColorSettings);
        }
    }
}
