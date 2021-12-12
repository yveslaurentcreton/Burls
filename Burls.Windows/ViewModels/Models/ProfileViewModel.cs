using Burls.Application.Profiles.Commands;
using Burls.Core.Data;
using Burls.Domain;
using Burls.Windows.Core;
using Burls.Windows.Services;
using MediatR;
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
        private readonly IBrowserStateNotificationService _browserStateNotificationService;
        private readonly IMediator _mediator;
        private readonly Profile _profile;

        public int Id => _profile.Id;
        public string Name => _profile.Name;
        public string IconPath => _profile.IconPath;

        public ObservableCollection<SelectionRuleViewModel> SelectionRules { get; }

        public ICommand AddSelectionRuleCommand { get; set; }

        public ProfileViewModel(IBrowserStateNotificationService browserStateNotificationService, IMediator mediator, Profile profile)
        {
            _browserStateNotificationService = browserStateNotificationService;
            _mediator = mediator;
            _profile = profile;

            SelectionRules = new ObservableCollection<SelectionRuleViewModel>(_profile.SelectionRules.Select(x => new SelectionRuleViewModel(mediator, x)));

            _browserStateNotificationService.SelectionRuleCreated += _browserStateNotificationService_SelectionRuleCreated;
            _browserStateNotificationService.SelectionRuleDeleted += _browserStateNotificationService_SelectionRuleDeleted;

            AddSelectionRuleCommand = new RelayCommand(async () => await AddNewSelectionRule());
        }

        public Task AddNewSelectionRule()
        {
            var command = new CreateProfileSelectionRuleCommand(Id, SelectionRuleParts.Url, SelectionRuleCompareTypes.Contains, "Value");

            return _mediator.Send(command);
        }

        private void _browserStateNotificationService_SelectionRuleCreated(object sender, SelectionRuleCreatedEventArgs e)
        {
            if (e.SelectionRule.ProfileId == Id)
            {
                SelectionRules.Add(new SelectionRuleViewModel(_mediator, e.SelectionRule));
            }
        }

        private void _browserStateNotificationService_SelectionRuleDeleted(object sender, SelectionRuleDeletedEventArgs e)
        {
            if (e.SelectionRule.ProfileId == Id)
            {
                var selectionRule = SelectionRules.Single(x => x.Id == e.SelectionRule.Id);
                SelectionRules.Remove(selectionRule);
            }
        }
    }
}
