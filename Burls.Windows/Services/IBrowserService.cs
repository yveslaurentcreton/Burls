using Burls.Domain;
using Burls.Windows.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public interface IBrowserService
    {
        Task InitializeBrowsersAsync();
        Task<IEnumerable<BrowserProfile>> GetBrowserProfilesAsync();
        Task UseBrowserProfileAsync(BrowserProfile browserProfile, string requestUrl, bool saveRequestUrl);
        Task UseBrowserProfileIndexAsync(IEnumerable<BrowserProfile> browserProfiles, string index, string requestUrl, bool saveRequestUrl);
    }
}