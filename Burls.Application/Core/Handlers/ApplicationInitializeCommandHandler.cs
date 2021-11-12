using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Commands;
using Burls.Application.Core.Services;
using Burls.Application.Core.State;
using Burls.Core.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Core.Handlers
{
    public class ApplicationInitializeCommandHandler : IRequestHandler<ApplicationInitializeCommand>
    {
        private readonly ILogger<ApplicationInitializeCommandHandler> _logger;
        private readonly IApplicationState _applicationStore;
        private readonly IBrowserState _browserState;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private readonly IBrowserService _browserService;

        public ApplicationInitializeCommandHandler(
            ILogger<ApplicationInitializeCommandHandler> logger,
            IApplicationState applicationStore,
            IBrowserState browserState,
            IPersistAndRestoreService persistAndRestoreService,
            IBrowserService browserService)
        {
            _logger = logger;
            _applicationStore = applicationStore;
            _browserState = browserState;
            _persistAndRestoreService = persistAndRestoreService;
            _browserService = browserService;
        }

        public async Task<Unit> Handle(ApplicationInitializeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initializing application");

            // Init vars
            var startUpArgs = request.StartUpArgs;
            var requestUrl = startUpArgs?.Skip(1).FirstOrDefault();
            var applicationMode = requestUrl.IsUrl() ? ApplicationMode.Select : ApplicationMode.Settings;

            _logger.LogInformation($"Determined startup arguments: {startUpArgs}");
            _logger.LogInformation($"Determined request url: {requestUrl}");
            _logger.LogInformation($"Determined application mode: {applicationMode}");

            // Init the application store
            _applicationStore.StartUpArgs = startUpArgs;
            _applicationStore.ApplicationMode = applicationMode;

            // Restore the saved data
            _logger.LogInformation($"Restoring settings");
            _persistAndRestoreService.InitDataDirectory();
            _persistAndRestoreService.RestoreData();

            // Init browsers
            _logger.LogInformation($"Initializing browsers with their profiles");
            await _browserService.InitializeBrowsersAsync();

            // Init the browser store
            _browserState.RequestUrl = requestUrl;
            _browserState.BrowserProfiles = _browserService.GetBrowserProfilesAsync().Result;
            _browserState.SelectedBrowserProfile = _browserState.BrowserProfiles?.FirstOrDefault();

            // Try to auto select
            if (_applicationStore.ApplicationMode == ApplicationMode.Select)
            {
                _logger.LogInformation($"Trying to determine the browser and profile to handle the requested url");

                var browserProfile = _browserState.BrowserProfiles
                    .FirstOrDefault(bp => bp.Profile.SelectionRules.Any(rule => rule.IsMatch(requestUrl)));

                if (browserProfile != null)
                {
                    _logger.LogInformation($"Using '{browserProfile.Name}' to handle the requested url");
                    await _browserService.UseBrowserProfileAsync(
                        browserProfile,
                        _browserState.RequestUrl,
                        false);
                }
                else
                {
                    _logger.LogInformation($"No matching browser found to handle the requested url");
                }
            }

            return Unit.Value;
        }
    }
}
