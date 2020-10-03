using Burls.Windows.Models;
using System.Collections.Generic;

namespace Burls.Windows.Services
{
    public interface IBrowserService
    {
        IEnumerable<Browser> GetBrowsers();
        IEnumerable<BrowserProfile> GetBrowserProfiles();
        void UseBrowserProfile(BrowserProfile browserProfile, string requestUrl);
        void UseBrowserProfileIndex(IEnumerable<BrowserProfile> browserProfiles, string index, string requestUrl);
    }
}