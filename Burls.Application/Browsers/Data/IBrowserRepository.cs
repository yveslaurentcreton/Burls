using Burls.Domain;
using Burls.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Browsers.Data
{
    public interface IBrowserRepository : IRepository<Browser>
    {
        IEnumerable<InstalledBrowser> GetInstalledBrowsers();
    }
}
