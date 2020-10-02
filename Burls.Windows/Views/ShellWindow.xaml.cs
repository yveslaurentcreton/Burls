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
            RegionManager.SetRegionName(hamburgerMenuContentControl, Regions.Main);
            RegionManager.SetRegionManager(hamburgerMenuContentControl, regionManager);

            Loaded += ShellWindow_Loaded;
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
