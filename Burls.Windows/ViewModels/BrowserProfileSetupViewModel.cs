using Burls.Application.Browsers.State;
using Burls.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.ViewModels
{
    public class BrowserProfileSetupViewModel : IViewModel
    {
        public IBrowserState BrowserState { get; set; }

        public BrowserProfileSetupViewModel(IBrowserState browserState)
        {
            BrowserState = browserState;
        }
    }
}
