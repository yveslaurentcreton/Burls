using Burls.Windows.Core;
using Burls.Windows.Pages;
using Burls.Windows.Services;
using Burls.Windows.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    public sealed partial class MainWindow : Window
    {
        private readonly ShellPage _shellPage;
        private readonly IServiceProvider _serviceProvider;
        private bool _shellPageLoaded;

        public MainWindow(ShellPage shellPage, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _shellPage = shellPage;
            _serviceProvider = serviceProvider;

            this.Activated += MainWindow_Activated;
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (!_shellPageLoaded)
            {
                Content = _shellPage;
                _shellPageLoaded = true;
            }
        }

        public void Navigate(string pageKey, Type pageType, Type viewModelType)
        {
            // Change content of the contentFrame
            _shellPage.ContentFrame.Navigate(pageType);
            var page = _shellPage.ContentFrame.Content as IBurlsPage;
            page.ViewModelBase = _serviceProvider.GetService(viewModelType) as IViewModel;
            _shellPage.RefreshUI();

            // Update the navigation if needed
            var menuItems = _shellPage.MainNavigation.MenuItems.Concat(_shellPage.MainNavigation.FooterMenuItems);
            var selectedItem = menuItems.FirstOrDefault(x => (x as NavigationViewItem).Tag.ToString().Equals(pageKey));

            if (_shellPage.MainNavigation.SelectedItem != selectedItem)
            {
                _shellPage.MainNavigation.SelectedItem = selectedItem;
            }
        }
    }
}
