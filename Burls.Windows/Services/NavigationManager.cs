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
        private readonly MainWindow _mainWindow;

        public NavigationManager(INavigationBroadcaster navigationBroadcaster, MainWindow mainWindow)
        {
            _navigationBroadcaster = navigationBroadcaster;
            _mainWindow = mainWindow;
        }

        private void _navigationBroadcaster_Navigating(object sender, NavigationEventArgs e)
        {
            var pageName = $"Burls.Windows.Pages.{e.PageName}";
            var pageType = Type.GetType(pageName);

            _mainWindow.Navigate(pageType);
        }

        public void Subscribe()
        {
            _navigationBroadcaster.Navigating += _navigationBroadcaster_Navigating;
        }
    }
}
