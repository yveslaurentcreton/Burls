using Burls.Windows.Models;
using Burls.Windows.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Burls.Windows.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IBrowserService _browserService;

        public string RequestUrl { get; set; }

        private IReadOnlyList<BrowserProfile> _browserProfiles;
        public IReadOnlyList<BrowserProfile> BrowserProfiles
        {
            get { return _browserProfiles; }
            set
            {
                _browserProfiles = value;
                RaisePropertyChanged();
            }
        }

        public ICommand UseCommand { get; set; }

        public MainViewModel(IBrowserService browserService)
        {
            _browserService = browserService;
            RequestUrl = (System.Windows.Application.Current as App).RequestUrl;
            BrowserProfiles = GetBrowserProfiles();

            UseCommand = new DelegateCommand<BrowserProfile>(Use);
        }

        private IReadOnlyList<BrowserProfile> GetBrowserProfiles()
        {
            var browsers = _browserService.GetBrowsers();
            var browserProfiles = new List<BrowserProfile>();

            foreach (var browser in browsers)
            {
                foreach (var profile in browser.Profiles)
                {
                    browserProfiles.Add(new BrowserProfile(browser, profile));
                }
            }

            return browserProfiles;
        }

        private void Use(BrowserProfile browserProfile)
        {
            browserProfile.NavigateToUrl(RequestUrl);
        }
    }
}