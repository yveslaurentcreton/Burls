using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class NavigationManager : INavigationManager
    {
        private readonly INavigationBroadcaster _navigationBroadcaster;
        private readonly INavigationStore _navigationStore;
        private readonly MainWindow _mainWindow;

        public NavigationManager(INavigationBroadcaster navigationBroadcaster, INavigationStore navigationStore, MainWindow mainWindow)
        {
            _navigationBroadcaster = navigationBroadcaster;
            _navigationStore = navigationStore;
            _mainWindow = mainWindow;
        }

        private void _navigationBroadcaster_Navigating(object sender, NavigationEventArgs e)
        {
            var pageType = _navigationStore.GetPageType(e.PageKey);
            var viewModelType = _navigationStore.GetViewModelType(pageType);

            _mainWindow.Navigate(e.PageKey, pageType, viewModelType);
        }

        public void Subscribe()
        {
            _navigationBroadcaster.Navigating += _navigationBroadcaster_Navigating;
        }
    }
}
