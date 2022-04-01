using Burls.Application.Browsers.Services;
using Burls.Domain;
using Burls.Domain.Core.Extensions;
using Burls.Windows.Core;
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
        private readonly IBrowserService _browserService;
        private readonly SelectionRule _selectionRule;
        private readonly Action<SelectionRule, SelectionRuleViewModel> _deleteSelectionRuleAction;

        public SelectionRuleParts SelectionRulePart { get { return _selectionRule.SelectionRulePart; } set { _selectionRule.SelectionRulePart = value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public int SelectionRulePartIndex { get { return (int)SelectionRulePart; } set { SelectionRulePart = (SelectionRuleParts)value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public SelectionRuleCompareTypes SelectionRuleCompareType { get { return _selectionRule.SelectionRuleCompareType; } set { _selectionRule.SelectionRuleCompareType = value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public int SelectionRuleCompareTypeIndex { get { return (int)SelectionRuleCompareType; } set { SelectionRuleCompareType = (SelectionRuleCompareTypes)value; UpdateSelectionRule(); OnPropertyChanged(); } }
        public string Value { get { return _selectionRule.Value; } set { _selectionRule.Value = value; UpdateSelectionRule(); OnPropertyChanged(); } }

        public ICommand DeleteSelectionRuleCommand { get; set; }

        public SelectionRuleViewModel(IBrowserService browserService, SelectionRule selectionRule, Action<SelectionRule, SelectionRuleViewModel> deleteSelectionRuleAction)
        {
            _browserService = browserService;
            _selectionRule = selectionRule;
            _deleteSelectionRuleAction = deleteSelectionRuleAction;
            DeleteSelectionRuleCommand = new RelayCommand(DeleteSelectionRule);
        }

        public bool IsMatch(string urlToMatch)
        {
            return _selectionRule.IsMatch(urlToMatch);
        }

        public void UpdateSelectionRule()
        {
            _browserService.UpdateSelectionRule(_selectionRule);
        }

        public void DeleteSelectionRule()
        {
            _deleteSelectionRuleAction(_selectionRule, this);
        }
    }
}
