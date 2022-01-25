using Burls.Application.Browsers.Services;
using Burls.Application.Core.State;
using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Browsers.State
{
    public class BrowserState : StateBase, IBrowserState
    {
        private readonly IBrowserService _browserService;

        private Lazy<IEnumerable<Browser>> _lazyBrowsers;
        private Lazy<IEnumerable<BrowserProfile>> _lazyBrowserProfiles;

        public string RequestUrl { get; set; }

        private bool _saveRequestUrl;
        public bool SaveRequestUrl
        {
            get { return _saveRequestUrl; }
            set { _saveRequestUrl = value; RaiseStateChanged(); }
        }

        public IEnumerable<Browser> Browsers { get => _lazyBrowsers.Value; }
        public IEnumerable<BrowserProfile> BrowserProfiles { get => _lazyBrowserProfiles.Value; }

        public BrowserState(IBrowserService browserService)
        {
            _browserService = browserService;

            CreateLazyBrowsers();
            CreateLazyBrowserProfiles();
        }

        private void CreateLazyBrowsers()
        {
            _lazyBrowsers = new Lazy<IEnumerable<Browser>>(_browserService.GetBrowsers());
        }

        private void CreateLazyBrowserProfiles()
        {
            _lazyBrowserProfiles = new Lazy<IEnumerable<BrowserProfile>>(_browserService.GetBrowserProfiles());
        }

        public void RefreshBrowsers()
        {
            CreateLazyBrowsers();
        }

        public void RefreshBrowserProfiles()
        {
            CreateLazyBrowserProfiles();
        }
    }
}
