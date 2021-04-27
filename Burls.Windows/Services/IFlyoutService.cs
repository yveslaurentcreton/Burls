using System.Windows.Input;

namespace Burls.Windows.Services
{
    public interface IFlyoutService
    {
        ICommand ShowFlyoutCommand { get; }

        void ShowFlyout(string flyoutName);
        bool CanShowFlyout(string flyoutName);
        void CloseFlyout(string flyoutName);
        bool CanCloseFlyout(string flyoutName);
    }
}