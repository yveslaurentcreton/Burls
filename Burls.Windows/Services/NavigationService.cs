using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigationBroadcaster _navigationBroadcaster;

        public NavigationService(INavigationBroadcaster navigationBroadcaster)
        {
            _navigationBroadcaster = navigationBroadcaster;
        }

        public void Navigate(string pageName)
        {
            _navigationBroadcaster.OnNavigating(new NavigationEventArgs(pageName));
        }
    }
}
