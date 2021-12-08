using Burls.Windows.Constants;
using Burls.Windows.Pages;
using Burls.Windows.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class NavigationStore : INavigationStore
    {
        private Dictionary<string, Type> _pageStore;
        private Dictionary<Type, Type> _viewModelStore;

        public NavigationStore()
        {
            _pageStore = new Dictionary<string, Type>
            {
                { PageKeys.BrowserProfileSelection, typeof(BrowserProfileSelectionPage) },
                { PageKeys.Settings, typeof(SettingsPage) }
            };

            _viewModelStore = new Dictionary<Type, Type>
            {
                { typeof(BrowserProfileSelectionPage), typeof(BrowserProfileSelectionViewModel) },
                { typeof(SettingsPage), typeof(SettingsViewModel) }
            };
        }

        public Type GetPageType(string pageKey)
        {
            _pageStore.TryGetValue(pageKey, out var pageType);
            return pageType;
        }

        public Type GetViewModelType(Type pageType)
        {
            _viewModelStore.TryGetValue(pageType, out var viewModelType);
            return viewModelType;
        }

        public Type GetViewModelType(string pageKey)
        {
            var pageType = GetPageType(pageKey);
            var viewModelType = GetViewModelType(pageType);
            return viewModelType;
        }
    }
}
