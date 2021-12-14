using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.State;
using Burls.Core.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public class ApplicationLifetimeService : IApplicationLifetimeService
    {
        private readonly ILogger<ApplicationLifetimeService> _logger;
        private readonly IApplicationState _applicationState;
        private readonly IBrowserState _browserState;
        private readonly IBrowserService _browserService;
        private readonly IPathService _pathService;

        public ApplicationLifetimeService(
            ILogger<ApplicationLifetimeService> logger,
            IApplicationState applicationState,
            IBrowserState browserState,
            IBrowserService browserService,
            IPathService pathService)
        {
            _logger = logger;
            _applicationState = applicationState;
            _browserState = browserState;
            _browserService = browserService;
            _pathService = pathService;
        }

        public async Task Initialize(string[] startUpArgs)
{
            _logger.LogInformation("Initializing application");

            // Init vars
            AppDomain.CurrentDomain.SetData("DataDirectory", _pathService.ApplicationDataFolder);

            var requestUrl = startUpArgs?.Skip(1).FirstOrDefault();
            var applicationMode = requestUrl.IsUrl() ? ApplicationMode.Select : ApplicationMode.Settings;

            _logger.LogInformation($"Determined startup arguments: {startUpArgs}");
            _logger.LogInformation($"Determined request url: {requestUrl}");
            _logger.LogInformation($"Determined application mode: {applicationMode}");

            // Init the application store
            _applicationState.StartUpArgs = startUpArgs;
            _applicationState.ApplicationMode = applicationMode;

            // Init browsers
            _logger.LogInformation($"Initializing browsers with their profiles");
            _browserService.SyncBrowsers();

            // Init the browser store
            _browserState.RequestUrl = requestUrl;

            // Try to auto select
            if (_applicationState.ApplicationMode == ApplicationMode.Select)
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
        }
    }
}
