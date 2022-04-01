using Burls.Application.Browsers.Services;
using Burls.Core.Data;
using Burls.Domain;
using Burls.Windows.Core;
using Burls.Windows.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using static Burls.Domain.SelectionRule;

namespace Burls.Windows.ViewModels.Models
{
    public class ProfileViewModel : ViewModelBase, IEntity
    {
        private readonly IBrowserService _browserService;
        private readonly Profile _profile;

        public string DisplayName => _profile.DisplayName;
        public string IconPath => _profile.IconPath;

        public ObservableCollection<SelectionRuleViewModel> SelectionRules { get; }

        public ICommand AddSelectionRuleCommand { get; set; }

        public ProfileViewModel(IBrowserService browserService, Profile profile)
        {
            _browserService = browserService;
            _profile = profile;

            SelectionRules = new ObservableCollection<SelectionRuleViewModel>(_profile.SelectionRules.Select(x => new SelectionRuleViewModel(browserService, x, DeleteSelectionRule)));

            AddSelectionRuleCommand = new RelayCommand(AddNewSelectionRule);
        }

        public void AddNewSelectionRule()
        {
            var newSelectionRule = _browserService.AddSelectionRule(_profile, SelectionRuleParts.Url, SelectionRuleCompareTypes.Contains, "Value");

            SelectionRules.Add(new SelectionRuleViewModel(_browserService, newSelectionRule, DeleteSelectionRule));
        }

        public void DeleteSelectionRule(SelectionRule selectionRule, SelectionRuleViewModel selectionRuleViewModel)
        {
            _browserService.DeleteSelectionRule(_profile, selectionRule);

            SelectionRules.Remove(selectionRuleViewModel);
        }
    }
}
