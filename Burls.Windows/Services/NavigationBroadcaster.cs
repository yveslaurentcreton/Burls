using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class NavigationBroadcaster : INavigationBroadcaster
    {
        public event EventHandler<NavigationEventArgs> Navigated;
        public event EventHandler<NavigationEventArgs> Navigating;

        public void OnNavigated(NavigationEventArgs navigationEventArgs)
        {
            Navigated?.Invoke(this, navigationEventArgs);
        }

        public void OnNavigating(NavigationEventArgs navigationEventArgs)
        {
            Navigating?.Invoke(this, navigationEventArgs);
        }
    }
}
