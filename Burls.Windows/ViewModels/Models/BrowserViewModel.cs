using Burls.Application.Browsers.Services;
using Burls.Core.Data;
using Burls.Domain;
using Burls.Windows.Core;
using Burls.Windows.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Burls.Windows.ViewModels.Models
{
    public class BrowserViewModel : ViewModelBase, IEntity
    {
        private readonly Browser _browser;

        public int Id => _browser.Id;
        public string Name => _browser.Name;
        public string ExecutablePath => _browser.ExecutablePath;
        public string IconPath => _browser.IconPath;
        public int IconIndex => _browser.IconIndex;
        public string ProfileArgumentName => _browser.ProfileArgumentName;

        public virtual ObservableCollection<ProfileViewModel> Profiles { get; protected set; }

        public BrowserViewModel(IBrowserService browserService, Browser browser)
        {
            _browser = browser;

            Profiles = new ObservableCollection<ProfileViewModel>(_browser.Profiles.Select(x => new ProfileViewModel(browserService, x)));
        }

        public string GetProfileArgument(Profile profile)
        {
            return _browser.GetProfileArgument(profile);
        }

        public void NavigateToUrl(string url, Profile profile)
        {
            _browser.NavigateToUrl(url, profile);
        }
    }
}
