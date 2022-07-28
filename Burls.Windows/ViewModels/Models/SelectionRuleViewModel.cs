using AutoMapper;
using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Domain;
using Burls.Domain.Core.Extensions;
using Burls.Windows.Core;
using Burls.Windows.ViewModels.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Burls.Domain.SelectionRule;

namespace Burls.Windows.ViewModels.Models
{
    public partial class SelectionRuleViewModel : ObservableRecipient
    {
        private readonly IBrowserState _browserState;
        private readonly IBrowserService _browserService;
        private readonly SelectionRule _selectionRule;
        public int SelectionRuleHashCode => _selectionRule.GetHashCode();
        
        [ObservableProperty]
        public SelectionRuleParts _selectionRulePart;
        [ObservableProperty]
        public int _selectionRulePartIndex;
        [ObservableProperty]
        public SelectionRuleCompareTypes _selectionRuleCompareType;
        [ObservableProperty]
        public int _selectionRuleCompareTypeIndex;
        [ObservableProperty]
        public string _value;

        public SelectionRuleViewModel(SelectionRule selectionRule)
        {
            _browserState = App.Current.GetService<IBrowserState>();
            _browserService = App.Current.GetService<IBrowserService>();
            _selectionRule = selectionRule;

            _selectionRulePart = _selectionRule.SelectionRulePart;
            _selectionRuleCompareType = _selectionRule.SelectionRuleCompareType;
            _value = _selectionRule.Value;

            IsActive = true;
        }

        protected override void OnActivated()
        {
            PropertyChanged += SelectionRuleViewModel_PropertyChanged;
        }

        private void SelectionRuleViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Update();
        }

        partial void OnSelectionRulePartChanged(SelectionRuleParts value) => _selectionRule.SelectionRulePart = value;
        partial void OnSelectionRulePartIndexChanged(int value) => SelectionRulePart = (SelectionRuleParts)value;
        partial void OnSelectionRuleCompareTypeChanged(SelectionRuleCompareTypes value) => _selectionRule.SelectionRuleCompareType = value;
        partial void OnSelectionRuleCompareTypeIndexChanged(int value) => SelectionRuleCompareType = (SelectionRuleCompareTypes)value;
        partial void OnValueChanged(string value) => _selectionRule.Value = value;

        [ICommand]
        public void Update()
        {
            _browserService.UpdateSelectionRule(_selectionRule);
        }

        [ICommand]
        public void Remove()
        {
            var profile = _browserState.BrowserProfiles.Select(bp => bp.Profile).Single(p => p.SelectionRules.Contains(_selectionRule));
            _browserService.DeleteSelectionRule(profile, _selectionRule);
            Messenger.Send<SelectionRuleRemoved>(new(_selectionRule));
        }

        protected override void OnDeactivated()
        {
            PropertyChanged -= SelectionRuleViewModel_PropertyChanged;
        }
    }
}
