using Burls.Application.Core.Services;
using Burls.Windows.Core;
using Burls.Windows.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Burls.Windows.Pages
{
    public sealed partial class ShellPage : UserControl, INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IApplicationService _applicationService;

        public Frame ContentFrame => ContentFrameInternal;
        public NavigationView MainNavigation => MainNavigationInternal;
        public IBurlsPage CurrentPage => ContentFrame.Content as IBurlsPage;

        public ShellPage(INavigationService navigationService, IApplicationService applicationService)
        {
            this.InitializeComponent();

            _navigationService = navigationService;
            _applicationService = applicationService;

            //this.Loaded += ShellPage_Loaded;
        }

        //private void ShellPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var theme = _applicationService.GetTheme();
        //    var themeName = theme.ToString();
        //    if (Enum.TryParse<ElementTheme>(themeName, out var elementTheme))
        //    {
        //        RequestedTheme = elementTheme;
        //        MainNavigation.RequestedTheme = elementTheme;
        //    }
        //}

        public void RefreshUI()
        {
            RaisePropertyChanged(nameof(CurrentPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            _navigationService.Navigate((args.SelectedItem as NavigationViewItem).Tag.ToString());
        }
    }
}
