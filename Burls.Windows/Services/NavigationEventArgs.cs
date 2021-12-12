using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class NavigationEventArgs : EventArgs
    {
        public string PageKey { get; set; }

        public NavigationEventArgs(string pageName)
        {
            PageKey = pageName;
        }
    }
}
