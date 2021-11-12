using Burls.Application.Browsers.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.ViewModels
{
    public class BrowserProfileSelectionViewModel
    {
        public IBrowserState BrowserState { get; set; }

        public BrowserProfileSelectionViewModel(IBrowserState browserState)
        {
            BrowserState = browserState;
        }
    }
}
