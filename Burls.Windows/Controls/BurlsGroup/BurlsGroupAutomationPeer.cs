using Microsoft.UI.Xaml.Automation.Peers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Controls
{
    public class BurlsGroupAutomationPeer : FrameworkElementAutomationPeer
    {
        public BurlsGroupAutomationPeer(BurlsGroup owner)
            : base(owner)
        {
        }

        protected override string GetNameCore()
        {
            var selectedSettingsGroup = (BurlsGroup)Owner;
            return selectedSettingsGroup.Header;
        }
    }
}
