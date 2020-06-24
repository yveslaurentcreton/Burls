using Burls.Windows.Models;
using System.Collections.Generic;

namespace Burls.Windows.Services
{
    public interface IBrowserService
    {
        IReadOnlyList<Browser> GetBrowsers();
    }
}