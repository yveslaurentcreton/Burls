using System;
using System.Windows.Input;
using Burls.Application.Core.Queries;
using Burls.Application.Core.Services;
using Burls.Windows.Contracts.Services;
using Burls.Windows.Models;
using MediatR;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Burls.Windows.ViewModels
{
    // TODO WTS: Change the URL for your privacy policy in the appsettings.json file, currently set to https://YourPrivacyUrlGoesHere
    public class SettingsViewModel : BindableBase, INavigationAware
    {
        private readonly IMediator _mediator;
        private readonly AppConfig _appConfig;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly ISystemService _systemService;
        private AppTheme _theme;
        private string _versionDescription;
        private ICommand _setThemeCommand;
        private ICommand _privacyStatementCommand;

        public AppTheme Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        public string VersionDescription
        {
            get { return _versionDescription; }
            set { SetProperty(ref _versionDescription, value); }
        }

        public ICommand SetThemeCommand => _setThemeCommand ?? (_setThemeCommand = new DelegateCommand<string>(OnSetTheme));

        public ICommand PrivacyStatementCommand => _privacyStatementCommand ?? (_privacyStatementCommand = new DelegateCommand(OnPrivacyStatement));

        public SettingsViewModel(
            IMediator mediator,
            AppConfig appConfig,
            IThemeSelectorService themeSelectorService,
            ISystemService systemService)
        {
            _mediator = mediator;
            _appConfig = appConfig;
            _themeSelectorService = themeSelectorService;
            _systemService = systemService;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var version = _mediator.Send(new GetApplicationVersionQuery()).Result.Version;
            VersionDescription = $"Burls - {version}";
            Theme = _themeSelectorService.GetCurrentTheme();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void OnSetTheme(string themeName)
        {
            var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
            _themeSelectorService.SetTheme(theme);
        }

        private void OnPrivacyStatement()
            => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;
    }
}
