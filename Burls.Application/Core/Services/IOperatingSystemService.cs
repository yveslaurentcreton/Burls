using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Services
{
    public interface IOperatingSystemService
    {
        void OpenColorSettings();
        void NavigateToUrl(BrowserProfile browserProfile, string url);
        void NavigateToUrl(Browser browser, Profile profile, string url);
    }
}
