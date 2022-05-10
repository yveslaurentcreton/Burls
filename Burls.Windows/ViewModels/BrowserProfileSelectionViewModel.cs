using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Services;
using Burls.Domain;
using Burls.Windows.Core;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.ViewModels
{
    public partial class BrowserProfileSelectionViewModel : IViewModel
    {
        private readonly IBrowserService _browserService;
        private readonly IApplicationService _applicationService;

        public IBrowserState BrowserState { get; set; }

        public BrowserProfileSelectionViewModel(IBrowserState browserState, IBrowserService browserService, IApplicationService applicationService)
        {
            BrowserState = browserState;
            _browserService = browserService;
            _applicationService = applicationService;
        }

        [ICommand]
        private void Shutdown() => _applicationService.Shutdown();

        [ICommand]
        private Task UseBrowserProfile(BrowserProfile browserProfile) => _browserService.UseBrowserProfileAsync(browserProfile, BrowserState.RequestUrl, BrowserState.SaveRequestUrl);
    }
}
