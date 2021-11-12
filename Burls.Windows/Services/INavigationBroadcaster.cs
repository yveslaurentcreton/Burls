using System;

namespace Burls.Windows.Services
{
    public interface INavigationBroadcaster
    {
        event EventHandler<NavigationEventArgs> Navigated;
        event EventHandler<NavigationEventArgs> Navigating;

        void OnNavigated(NavigationEventArgs navigationEventArgs);
        void OnNavigating(NavigationEventArgs navigationEventArgs);
    }
}