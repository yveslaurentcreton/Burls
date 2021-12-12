using Burls.Application.Profiles.Commands;
using Burls.Domain;
using Burls.Domain.Core.Extensions;
using Burls.Windows.Core;
using MediatR;
using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Burls.Domain.SelectionRule;

namespace Burls.Windows.ViewModels.Models
{
    public class SelectionRuleViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly SelectionRule _selectionRule;

        public int Id => _selectionRule.Id;
        public int ProfileId => _selectionRule.ProfileId;
        public SelectionRuleParts SelectionRulePart { get { return _selectionRule.SelectionRulePart; } set { _selectionRule.SelectionRulePart = value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public int SelectionRulePartIndex { get { return (int)SelectionRulePart; } set { SelectionRulePart = (SelectionRuleParts)value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public SelectionRuleCompareTypes SelectionRuleCompareType { get { return _selectionRule.SelectionRuleCompareType; } set { _selectionRule.SelectionRuleCompareType = value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public int SelectionRuleCompareTypeIndex { get { return (int)SelectionRuleCompareType; } set { SelectionRuleCompareType = (SelectionRuleCompareTypes)value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public string Value { get { return _selectionRule.Value; } set { _selectionRule.Value = value; UpdateSelectionRule(); OnPropertyChanged(); } }

        public ICommand DeleteSelectionRuleCommand { get; set; }

        public SelectionRuleViewModel(IMediator mediator, SelectionRule selectionRule)
        {
            _mediator = mediator;
            _selectionRule = selectionRule;

            DeleteSelectionRuleCommand = new RelayCommand(async () => await DeleteSelectionRule());
        }

        public bool IsMatch(string urlToMatch)
        {
            return _selectionRule.IsMatch(urlToMatch);
        }

        public void UpdateSelectionRule()
        {
            Task.Run(() => {
                var command = new UpdateProfileSelectionRuleCommand(ProfileId, Id, _selectionRule.SelectionRulePart, _selectionRule.SelectionRuleCompareType, Value);

                return _mediator.Send(command);
            }).Wait();
        }

        public Task DeleteSelectionRule()
        {
            var command = new DeleteProfileSelectionRuleCommand(ProfileId, Id);

            return _mediator.Send(command);
        }
    }
}
