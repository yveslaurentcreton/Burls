using Burls.Domain;
using Burls.Windows.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Burls.Application.Browsers.Services
{
    public interface IBrowserService
    {
        IEnumerable<Browser> GetBrowsers();
        IEnumerable<BrowserProfile> GetBrowserProfiles();
        void SyncBrowsers();
        SelectionRule AddSelectionRule(Profile profile, SelectionRule.SelectionRuleParts selectionRulePart, SelectionRule.SelectionRuleCompareTypes selectionRuleCompareType, string value);
        void UpdateSelectionRule(SelectionRule selectionRule);
        void DeleteSelectionRule(Profile profile, SelectionRule selectionRule);
        Task UseBrowserProfileAsync(BrowserProfile browserProfile, string requestUrl, bool saveRequestUrl);
        Task UseBrowserProfileIndexAsync(IEnumerable<BrowserProfile> browserProfiles, string index, string requestUrl, bool saveRequestUrl);
    }
}