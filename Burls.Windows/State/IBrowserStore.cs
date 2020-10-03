using Burls.Windows.Models;
using System.Collections.Generic;

namespace Burls.Windows.State
{
    public interface IBrowserStore
    {
        IEnumerable<BrowserProfile> BrowserProfiles { get; set; }
        string RequestUrl { get; set; }
    }
}