using Burls.Windows.Services;
using Burls.Windows.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Burls.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : BurlsWindow
    {
        public MainViewModel ViewModel { get; set; }

        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(MainViewModel viewModel, INavigationService navigationService, IServiceProvider serviceProvider)
        {
            ViewModel = viewModel;

            InitializeComponent();

            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
        }

        public void Navigate(Type pageType)
        {
            // Change content of the contentFrame
            ContentFrame.Navigate(pageType);
            var page = ContentFrame.Content as IBurlsPage;
            page.SetViewModel(_serviceProvider.GetService(typeof(BrowserProfileSelectionViewModel)));

            // Update the navigation if needed
            var menuItems = MainNavigation.MenuItems.Concat(MainNavigation.FooterMenuItems);
            var selectedItem = menuItems.FirstOrDefault(x => (x as NavigationViewItem).Tag.ToString().Equals(pageType.Name));

            if (MainNavigation.SelectedItem != selectedItem)
            {
                MainNavigation.SelectedItem = selectedItem;
            }
        }

        private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            _navigationService.Navigate((args.SelectedItem as NavigationViewItem).Tag.ToString());
        }
    }
}
