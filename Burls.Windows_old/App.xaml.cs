using Burls.Windows_old.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Burls.Windows_old
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public string RequestUrl { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            RequestUrl = e.Args?.FirstOrDefault();

            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IBrowserService, BrowserService>();
        }
    }
}
