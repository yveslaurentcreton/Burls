using Burls.Domain;
using Burls.Windows.Core;
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
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Burls.Windows.Pages
{
    public sealed partial class BrowserProfileSelectionPage : Page, IBurlsPage
    {
        public string Title => "Select browser";
        public BrowserProfileSelectionViewModel ViewModel => ViewModelBase as BrowserProfileSelectionViewModel;
        public IViewModel ViewModelBase { get; set; }

        public BrowserProfileSelectionPage()
        {
            this.InitializeComponent();
        }
    }
}