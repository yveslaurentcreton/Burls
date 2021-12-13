using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public interface IUpdateService
    {
        Version GetVersion();
        Task<(Version Version, string Name, string DownloadUrl)> GetLatestVersionInfo();
        Task<VersionStatus> GetVersionStatus();
        Task DownloadLatestVersion();
        Task<LatestVersionStatus> GetLatestVersionStatus();
        Task InstallLatestVersion();
    }

    public enum VersionStatus
    {
        OldVersion = -1,
        CurrentVersion = 0,
        FutureVersion = 1
    }

    public enum LatestVersionStatus
    {
        None = 0,
        Downloaded = 1
    }
}
