using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Commands;
using Burls.Application.Core.Services;
using Burls.Domain;
using Burls.Windows.Constants;
using Burls.Windows.Events;
using MediatR;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Burls.Windows.ViewModels
{
    public class BrowserProfileSelectionViewModel : BindableBase, INavigationAware
    {
        private readonly IMediator _mediator;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionNavigationService _navigationService;
        private readonly IBrowserService _browserService;
        public readonly IBrowserState _browserState;
        private SubscriptionToken _commandHandlerToken;

        public string RequestUrl => _browserState.RequestUrl;
        public IEnumerable<BrowserProfile> BrowserProfiles => _browserState.BrowserProfiles;

        public bool SaveRequestUrl
        {
            get { return _browserState.SaveRequestUrl; }
            set { _browserState.SaveRequestUrl = value; }
        }

        public ICommand UseBrowserProfileCommand { get; }

        public BrowserProfileSelectionViewModel(IMediator mediator, IRegionManager regionManager, IEventAggregator eventAggregator, IBrowserService browserService, IBrowserState browserState)
        {
            _mediator = mediator;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _navigationService = regionManager.Regions[Regions.Main].NavigationService;
            _browserService = browserService;
            _browserState = browserState;

            _browserState.StateChanged += _browserState_StateChanged;

            UseBrowserProfileCommand = new DelegateCommand<BrowserProfile>(async (browserProfile) => await UseBrowserProfile(browserProfile));
        }

        private void _browserState_StateChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(SaveRequestUrl));
            RaisePropertyChanged(nameof(BrowserProfiles));
        }

        private void Subscribe()
        {
            _commandHandlerToken = _eventAggregator.GetEvent<CommandTriggerEvent>().Subscribe(async (e) => await CommandHandler(e));
        }

        private void Unsubscribe()
        {
            _eventAggregator.GetEvent<CommandTriggerEvent>().Unsubscribe(_commandHandlerToken);
        }

        private async Task CommandHandler(CommandTriggerEventArgs e)
        {
             var commandParameterParts = e.CommandParameter.Split('.');

            if (commandParameterParts[0].Equals("BrowserProfile"))
            {
                if (commandParameterParts[1].Equals("Select"))
                {
                    var browserProfileIndex = commandParameterParts[2];

                    await _browserService.UseBrowserProfileIndexAsync(
                        _browserState.BrowserProfiles,
                        browserProfileIndex,
                        _browserState.RequestUrl,
                        _browserState.SaveRequestUrl);
                }

                if (commandParameterParts[1].Equals("SaveRequest"))
                {
                    _browserState.SaveRequestUrl = !_browserState.SaveRequestUrl;
                }
            }

            if (commandParameterParts[0].Equals("Application")
            &&  commandParameterParts[1].Equals("Shutdown"))
            {
                await _mediator.Send(new ApplicationShutdownCommand());
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Subscribe();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Unsubscribe();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        private Task UseBrowserProfile(BrowserProfile browserProfile)
        {
            return _browserService.UseBrowserProfileAsync(browserProfile, _browserState.RequestUrl, _browserState.SaveRequestUrl);
        }
    }
}
