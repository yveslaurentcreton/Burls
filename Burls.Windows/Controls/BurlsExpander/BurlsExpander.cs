using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Controls.BurlsExpander
{
    public class BurlsExpander : Expander
    {
        public BurlsExpander()
        {
            DefaultStyleKey = typeof(Expander);
            this.Style = (Style)App.Current.Resources["SettingExpanderStyle"];
            this.RegisterPropertyChangedCallback(Expander.HeaderProperty, OnHeaderChanged);
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyProperty dp)
        {
            BurlsExpander self = (BurlsExpander)d;
            if (self.Header != null)
            {
                if (self.Header.GetType() == typeof(BurlsItem))
                {
                    BurlsItem selfSetting = (BurlsItem)self.Header;
                    selfSetting.Style = (Style)App.Current.Resources["ExpanderHeaderSettingStyle"];

                    if (!string.IsNullOrEmpty(selfSetting.Header))
                    {
                        AutomationProperties.SetName(self, selfSetting.Header);
                    }
                }
            }
        }
    }
}
