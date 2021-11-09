using Burls.Windows.Constants;

using MahApps.Metro.Controls;

using Prism.Regions;
using System.Windows;

namespace Burls.Windows.Views
{
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            Loaded += ShellWindow_Loaded;

            SetRegionManager(regionManager, hamburgerMenuContentControl, Regions.Main);
            SetRegionManager(regionManager, flyoutsControlRegion, Regions.FlyoutRegion);
        }

        private void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }

        private void ShellWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BringToTheFront();
        }

        private void BringToTheFront()
        {
            Topmost = true;
            Topmost = false;
            Focus();
        }
    }
}
