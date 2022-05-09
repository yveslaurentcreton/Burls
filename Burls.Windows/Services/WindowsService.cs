using AutoMapper;
using Burls.Application.Core.Services;
using Burls.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class WindowsService : IOperatingSystemService
    {
        private readonly ILogger<WindowsService> _logger;

        public WindowsService(ILogger<WindowsService> logger)
        {
            _logger = logger;
        }

        public void OpenColorSettings()
        {
            Process.Start(new ProcessStartInfo("ms-settings:colors") { UseShellExecute = true });
        }

        public void NavigateToUrl(BrowserProfile browserProfile, string url)
{
            NavigateToUrl(browserProfile.Browser, browserProfile.Profile, url);
        }

        public void NavigateToUrl(Browser browser, Domain.Profile profile, string url)
        {
            var profileArgument = browser.GetProfileArgument(profile);
            var urlArgument = $"\"{url}\"";
            var argumentList = new List<string>() { profileArgument, urlArgument };
            var arguments = string.Join(' ', argumentList);

            _logger.LogInformation($"Navigating to {url} using: {browser.ExecutablePath} {arguments}");
            Process.Start(browser.ExecutablePath, arguments);
        }
    }
}
