using Burls.Application.Core.State;
using Burls.Domain;
using Burls.Windows.Models;
using System.Collections.Generic;

namespace Burls.Application.Browsers.State
{
    public interface IBrowserState : IState
    {
        string RequestUrl { get; set; }
        bool SaveRequestUrl { get; set; }
        IEnumerable<BrowserProfile> BrowserProfiles { get; }
    }
}