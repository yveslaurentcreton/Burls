using Burls.Windows.Core;
using Burls.Windows.Pages;
using Burls.Windows.Services;
using Burls.Windows.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PInvoke;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using static PInvoke.User32;

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

            Activated += MainWindow_Activated;

            SetWindowDetails(1180, 860);
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

        private void SetWindowDetails(int width, int height)
        {
            // Get window info
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var dpi = GetDpiForWindow(hwnd);
            float scalingFactor = (float)dpi / 96;
            width = (int)(width * scalingFactor);
            height = (int)(height * scalingFactor);

            // Get monitor info
            var monitor = MonitorFromWindow(hwnd, MonitorOptions.MONITOR_DEFAULTTONEAREST);
            GetMonitorInfo(monitor, out var info);
            int monitor_width = info.Monitor.right - info.Monitor.left;
            int monitor_height = info.Monitor.bottom - info.Monitor.top;

            // Calc new position (Center screen)
            Point pt = new Point((monitor_width - width) / 2, (monitor_height - height) / 2);

            // Set new position
            SetWindowPos(hwnd, SpecialWindowHandles.HWND_TOPMOST, (int)pt.X, (int)pt.Y, width, height, SetWindowPosFlags.SWP_SHOWWINDOW);
        }
    }
}
