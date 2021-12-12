using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Burls.Windows.Controls
{
    /// <summary>
    /// Represents a control that can contain multiple settings (or other) controls
    /// </summary>
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
    public partial class BurlsGroup : ItemsControl
    {
        public BurlsGroup()
        {
            DefaultStyleKey = typeof(BurlsGroup);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(string),
            typeof(BurlsGroup),
            new PropertyMetadata(default(string)));

        [Localizable(true)]
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            IsEnabledChanged -= SettingsGroup_IsEnabledChanged;
            SetEnabledState();
            IsEnabledChanged += SettingsGroup_IsEnabledChanged;
            base.OnApplyTemplate();
        }

        private void SettingsGroup_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetEnabledState();
        }

        private void SetEnabledState()
        {
            VisualStateManager.GoToState(this, IsEnabled ? "Normal" : "Disabled", true);
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new BurlsGroupAutomationPeer(this);
        }
    }
}
