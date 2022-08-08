using Burls.Application.Browsers.Services;
using Burls.Core.Data;
using Burls.Domain;
using Burls.Windows.Core;
using Burls.Windows.Services;
using Burls.Windows.ViewModels.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Burls.Domain.SelectionRule;

namespace Burls.Windows.ViewModels.Models
{
    public partial class ProfileViewModel : ObservableRecipient, IEntity
    {
        private readonly IBrowserService _browserService;
        private readonly Profile _profile;

        public string DisplayName => _profile.DisplayName;
        public string IconPath => _profile.IconPath;

        public ObservableCollection<SelectionRuleViewModel> SelectionRules { get; }

        public ProfileViewModel(Profile profile)
        {
            _browserService = App.Current.GetService<IBrowserService>();
            _profile = profile;

            SelectionRules = new ObservableCollection<SelectionRuleViewModel>(_profile.SelectionRules.Select(x => new SelectionRuleViewModel(x)));

            IsActive = true;
        }

        protected override void OnActivated()
        {
            Messenger.Register<ProfileViewModel, SelectionRuleRemoved>(this, RemoveSelectionRule);
        }

        private void RemoveSelectionRule(ProfileViewModel recipient, SelectionRuleRemoved message)
        {
            var selectionRule = SelectionRules.FirstOrDefault(viewModel => viewModel.SelectionRuleHashCode == message.SelectionRule.GetHashCode());

            if (selectionRule != null)
                SelectionRules.Remove(selectionRule);
        }

        [RelayCommand]
        public void CreateSelectionRule()
        {
            var selectionRule = _browserService.AddSelectionRule(_profile, SelectionRuleParts.Url, SelectionRuleCompareTypes.Contains, "Value");

            SelectionRules.Add(new SelectionRuleViewModel(selectionRule));
        }

        protected override void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }
    }
}
