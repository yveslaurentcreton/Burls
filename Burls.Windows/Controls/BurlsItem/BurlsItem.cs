using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
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
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
    [TemplatePart(Name = PartIconPresenter, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = PartDescriptionPresenter, Type = typeof(ContentPresenter))]
    public class BurlsItem : ContentControl
    {
        private const string PartIconPresenter = "IconPresenter";
        private const string PartDescriptionPresenter = "DescriptionPresenter";
        private ContentPresenter _iconPresenter;
        private ContentPresenter _descriptionPresenter;
        private BurlsItem _burlsItem;

        public BurlsItem()
        {
            this.DefaultStyleKey = typeof(BurlsItem);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
           "Header",
           typeof(string),
           typeof(BurlsItem),
           new PropertyMetadata(default(string), OnHeaderChanged));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            "Description",
            typeof(object),
            typeof(BurlsItem),
            new PropertyMetadata(null, OnDescriptionChanged));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon",
            typeof(object),
            typeof(BurlsItem),
            new PropertyMetadata(default(string), OnIconChanged));

        public static readonly DependencyProperty ActionContentProperty = DependencyProperty.Register(
            "ActionContent",
            typeof(object),
            typeof(BurlsItem),
            null);

        [Localizable(true)]
        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        [Localizable(true)]
        public object Description
        {
            get => (object)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public object Icon
        {
            get => (object)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public object ActionContent
        {
            get => (object)GetValue(ActionContentProperty);
            set => SetValue(ActionContentProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            IsEnabledChanged -= Setting_IsEnabledChanged;
            _burlsItem = (BurlsItem)this;
            _iconPresenter = (ContentPresenter)_burlsItem.GetTemplateChild(PartIconPresenter);
            _descriptionPresenter = (ContentPresenter)_burlsItem.GetTemplateChild(PartDescriptionPresenter);
            Update();
            SetEnabledState();
            IsEnabledChanged += Setting_IsEnabledChanged;
            base.OnApplyTemplate();
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BurlsItem)d).Update();
        }

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BurlsItem)d).Update();
        }

        private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BurlsItem)d).Update();
        }

        private void Setting_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetEnabledState();
        }

        private void SetEnabledState()
        {
            VisualStateManager.GoToState(this, IsEnabled ? "Normal" : "Disabled", true);
        }

        private void Update()
        {
            if (_burlsItem == null)
            {
                return;
            }

            if (_burlsItem.ActionContent != null)
            {
                if (_burlsItem.ActionContent.GetType() != typeof(Button))
                {
                    // We do not want to override the default AutomationProperties.Name of a button. Its Content property already describes what it does.
                    if (!string.IsNullOrEmpty(_burlsItem.Header))
                    {
                        AutomationProperties.SetName((UIElement)_burlsItem.ActionContent, _burlsItem.Header);
                    }
                }
            }

            if (_burlsItem._iconPresenter != null)
            {
                if (_burlsItem.Icon == null)
                {
                    _burlsItem._iconPresenter.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _burlsItem._iconPresenter.Visibility = Visibility.Visible;
                }
            }

            if (_burlsItem.Description == null)
            {
                _burlsItem._descriptionPresenter.Visibility = Visibility.Collapsed;
            }
            else
            {
                _burlsItem._descriptionPresenter.Visibility = Visibility.Visible;
            }
        }
    }
}
