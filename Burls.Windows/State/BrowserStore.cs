using Burls.Windows.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.State
{
    public class BrowserStore : BindableBase, IBrowserStore
    {
        public string RequestUrl { get; set; }

        private IEnumerable<BrowserProfile> _browserProfiles;
        public IEnumerable<BrowserProfile> BrowserProfiles
        {
            get { return _browserProfiles; }
            set
            {
                _browserProfiles = value;
                RaisePropertyChanged();
            }
        }
    }
}
