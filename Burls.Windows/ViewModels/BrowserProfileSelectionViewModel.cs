using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Services;
using Burls.Domain;
using Burls.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Burls.Windows.ViewModels
{
    public class BrowserProfileSelectionViewModel : ViewModelBase, IViewModel
    {
        private readonly IBrowserService _browserService;
        private readonly IApplicationService _applicationService;

        public IBrowserState BrowserState { get; set; }
        public ICommand ShutdownCommand { get; }
        public ICommand UseBrowserProfileCommand { get; }

        public BrowserProfileSelectionViewModel(IBrowserState browserState, IBrowserService browserService, IApplicationService applicationService)
        {
            BrowserState = browserState;
            _browserService = browserService;
            _applicationService = applicationService;

            ShutdownCommand = new RelayCommand(_applicationService.Shutdown);
            UseBrowserProfileCommand = new RelayCommand<BrowserProfile>(async (browserProfile) => await UseBrowserProfile(browserProfile));
        }

        private Task UseBrowserProfile(BrowserProfile browserProfile)
        {
            return _browserService.UseBrowserProfileAsync(browserProfile, BrowserState.RequestUrl, BrowserState.SaveRequestUrl);
        }
    }
}
