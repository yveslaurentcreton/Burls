using Burls.Application.Browsers.State;
using Burls.Application.Profiles.Commands;
using Burls.Domain;
using Burls.Windows.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Burls.Domain.SelectionRule;
using System.Windows.Input;
using System.Diagnostics;

namespace Burls.Windows.ViewModels
{
    public class SettingsViewModel : IViewModel
    {
        private readonly IMediator _mediator;

        public IBrowserState BrowserState { get; set; }

        public ICommand OpenWindowsColorSettingsCommand { get; set; }
        public ICommand AddSelectionRuleCommand { get; set; }
        public ICommand DeleteSelectionRuleCommand { get; set; }

        public SettingsViewModel(IBrowserState browserState, IMediator mediator)
        {
            BrowserState = browserState;
            _mediator = mediator;

            OpenWindowsColorSettingsCommand = new RelayCommand(OpenWindowsColorSettings);
            AddSelectionRuleCommand = new RelayCommand<BrowserProfile>(async (browserProfile) => await AddNewRule(browserProfile));
            DeleteSelectionRuleCommand = new RelayCommand<SelectionRule>(async (selectionRule) => await DeleteRule(selectionRule));
        }

        private void OpenWindowsColorSettings()
        {
            Process.Start(new ProcessStartInfo("ms-settings:colors") { UseShellExecute = true });
        }

        public Task AddNewRule(BrowserProfile browserProfile)
        {
            var command = new CreateProfileSelectionRuleCommand(browserProfile.Profile.Id, SelectionRuleParts.Url, SelectionRuleCompareTypes.Contains, "Value");

            return _mediator.Send(command);
        }

        public Task DeleteRule(SelectionRule selectionRule)
        {
            var command = new DeleteProfileSelectionRuleCommand(selectionRule.ProfileId, selectionRule.Id);

            return _mediator.Send(command);
        }
    }
}
