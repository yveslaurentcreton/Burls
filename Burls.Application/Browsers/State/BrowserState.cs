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
        public string RequestUrl { get; set; }

        private bool _saveRequestUrl;
        public bool SaveRequestUrl
        {
            get { return _saveRequestUrl; }
            set { _saveRequestUrl = value; RaiseStateChanged(); }
        }

        private IEnumerable<BrowserProfile> _browserProfiles;
        public IEnumerable<BrowserProfile> BrowserProfiles
        {
            get { return _browserProfiles; }
            set { _browserProfiles = value; RaiseStateChanged(); }
        }

        private BrowserProfile _selectedBrowserProfile;
        public BrowserProfile SelectedBrowserProfile
        {
            get { return _selectedBrowserProfile; }
            set { _selectedBrowserProfile = value; RaiseStateChanged(); }
        }

        private SelectionRule _selectedSelectionRule;
        public SelectionRule SelectedSelectionRule
        {
            get { return _selectedSelectionRule; }
            set { _selectedSelectionRule = value; RaiseStateChanged(); }
        }
    }
}
