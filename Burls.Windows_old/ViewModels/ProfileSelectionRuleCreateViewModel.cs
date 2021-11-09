using Burls.Application.Browsers.State;
using Burls.Application.Profiles.Commands;
using Burls.Domain;
using Burls.Windows.Constants;
using Burls.Windows.Services;
using MediatR;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Burls.Domain.SelectionRule;

namespace Burls.Windows.ViewModels
{
    public class ProfileSelectionRuleCreateViewModel : BindableBase
    {
        private readonly IMediator _mediator;
        private readonly IBrowserState _browserState;
        private readonly IFlyoutService _flyoutService;

        // Fields
        public SelectionRuleParts SelectionRulePart { get; set; }
        public SelectionRuleCompareTypes SelectionRuleCompareType { get; set; }
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; _okCommand.RaiseCanExecuteChanged(); }
        }

        // Commands
        private readonly DelegateCommand _okCommand;
        public ICommand OkCommand => _okCommand;
        private readonly DelegateCommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand;

        public ProfileSelectionRuleCreateViewModel(IMediator mediator, IBrowserState browserState, IFlyoutService flyoutService)
        {
            _mediator = mediator;
            _browserState = browserState;
            _flyoutService = flyoutService;

            _okCommand = new DelegateCommand(async () => await OkAsync(), () => CanOk());
            _cancelCommand = new DelegateCommand(Cancel);
        }

        private void SetSelectionRulePart(SelectionRuleParts selectionRulePart)
        {
            SelectionRulePart = selectionRulePart;
            RaisePropertyChanged(nameof(SelectionRulePart));
        }

        private void SetSelectionRuleCompareType(SelectionRuleCompareTypes selectionRuleCompareType)
        {
            SelectionRuleCompareType = selectionRuleCompareType;
            RaisePropertyChanged(nameof(SelectionRuleCompareType));
        }

        private void SetValue(string value)
        {
            Value = value;
            RaisePropertyChanged(nameof(Value));
        }

        private bool CanOk()
        {
            return !string.IsNullOrEmpty(Value);
        }

        private async Task OkAsync()
        {
            var profileId = _browserState.SelectedBrowserProfile.Profile.Id;
            var command = new CreateProfileSelectionRuleCommand(profileId, SelectionRulePart, SelectionRuleCompareType, Value);

            await _mediator.Send(command);

            Close();
        }

        private void Cancel()
        {
            Close();
        }

        private void Close()
        {
            // Clear
            SetSelectionRulePart(default);
            SetSelectionRuleCompareType(default);
            SetValue(default);

            // Close
            _flyoutService.CloseFlyout(FlyoutNames.ProfileSelectionRuleCreate);
        }
    }
}
