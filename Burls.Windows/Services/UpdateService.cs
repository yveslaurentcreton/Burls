using Burls.Application.Core.Services;
using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IApplicationService _applicationService;

        public UpdateService(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public Version GetVersion()
        {
            return _applicationService.GetVersion();
        }

        public async Task<(Version Version, string Name, string DownloadUrl)> GetLatestVersionInfo()
        {
            var client = new GitHubClient(new ProductHeaderValue("BEzGumpTion"));
            var releases = await client.Repository.Release.GetAll("BEzGumpTion", "Burls");
            var latestRelease = releases.FirstOrDefault();
            var asset = latestRelease.Assets.FirstOrDefault();

            return (Version: new Version(latestRelease.TagName), Name: asset?.Name, DownloadUrl: asset?.BrowserDownloadUrl);
        }

        public async Task<VersionStatus> GetVersionStatus()
        {
            var latestGitHubVersionInfo = await GetLatestVersionInfo();
            var localVersion = GetVersion();

            return (VersionStatus)localVersion.CompareTo(latestGitHubVersionInfo.Version);
        }

        public async Task DownloadLatestVersion()
        {
            var latestVersionInfo = await GetLatestVersionInfo();
            var downloadUrl = latestVersionInfo.DownloadUrl;
            var targetDirectoryName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Burls", "Downloads", latestVersionInfo.Version.ToString());
            var targetFilename = Path.Combine(targetDirectoryName, latestVersionInfo.Name);

            var client = new HttpClient();
            var response = await client.GetAsync(downloadUrl);

            if (!Directory.Exists(targetDirectoryName))
                Directory.CreateDirectory(targetDirectoryName);

            using var fileStream = new FileStream(targetFilename, System.IO.FileMode.Create);

            await response.Content.CopyToAsync(fileStream);
        }

        public async Task<LatestVersionStatus> GetLatestVersionStatus()
        {
            var latestVersionInfo = await GetLatestVersionInfo();
            var targetDirectoryName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Burls", "Downloads", latestVersionInfo.Version.ToString());
            var targetFilename = Path.Combine(targetDirectoryName, latestVersionInfo.Name);
            var updateDownloaded = File.Exists(targetFilename);

            return updateDownloaded ? LatestVersionStatus.Downloaded : LatestVersionStatus.None;
        }

        public async Task InstallLatestVersion()
        {
            var latestVersionInfo = await GetLatestVersionInfo();
            var targetDirectoryName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Burls", "Downloads", latestVersionInfo.Version.ToString());
            var targetFilename = Path.Combine(targetDirectoryName, latestVersionInfo.Name);

            Process.Start(new ProcessStartInfo(targetFilename) { UseShellExecute = true });

            _applicationService.Shutdown();
        }
    }
}
