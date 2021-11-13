using System;

namespace Burls.Windows.Services
{
    public interface INavigationStore
    {
        Type GetPageType(string pageKey);
        Type GetViewModelType(string pageKey);
        Type GetViewModelType(Type pageType);
    }
}