using Burls.Windows.Constants;
using Burls.Windows.Core;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Burls.Windows.Services
{
    public class FlyoutService : IFlyoutService
    {
        private readonly IRegionManager _regionManager;

        public ICommand ShowFlyoutCommand { get; private set; }
        public ICommand CloseFlyoutCommand { get; private set; }

        public FlyoutService(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            _regionManager = regionManager;

            ShowFlyoutCommand = new DelegateCommand<string>(ShowFlyout, CanShowFlyout);
            CloseFlyoutCommand = new DelegateCommand<string>(CloseFlyout, CanCloseFlyout);

            applicationCommands.ShowFlyoutCommand.RegisterCommand(ShowFlyoutCommand);
            applicationCommands.CloseFlyoutCommand.RegisterCommand(CloseFlyoutCommand);
        }

        public void ShowFlyout(string flyoutName)
        {
            ChangeFlyoutIsOpen(flyoutName, true);
        }

        public bool CanShowFlyout(string flyoutName)
        {
            return true;
        }

        public void CloseFlyout(string flyoutName)
        {
            ChangeFlyoutIsOpen(flyoutName, false);
        }

        public bool CanCloseFlyout(string flyoutName)
        {
            return true;
        }

        private void ChangeFlyoutIsOpen(string flyoutName, bool isOpen)
        {
            var region = _regionManager.Regions[Regions.FlyoutRegion];

            if (region != null)
            {
                var flyout = region.Views.Where(v => v is IFlyoutView && ((IFlyoutView)v).FlyoutName.Equals(flyoutName)).FirstOrDefault() as Flyout;

                if (flyout != null)
                {
                    flyout.IsOpen = isOpen;
                }
            }
        }
    }
}
