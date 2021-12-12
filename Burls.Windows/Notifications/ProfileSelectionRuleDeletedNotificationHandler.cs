using Burls.Application.Profiles.Notifications;
using Burls.Windows.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Windows.Notifications
{
    public class ProfileSelectionRuleDeletedNotificationHandler : INotificationHandler<ProfileSelectionRuleDeletedNotification>
    {
        private readonly IBrowserStateNotificationService _browserStateNotificationService;

        public ProfileSelectionRuleDeletedNotificationHandler(IBrowserStateNotificationService browserStateNotificationService)
        {
            _browserStateNotificationService = browserStateNotificationService;
        }

        public Task Handle(ProfileSelectionRuleDeletedNotification notification, CancellationToken cancellationToken)
        {
            _browserStateNotificationService.RaiseSelectionRuleDeleted(notification.SelectionRule);

            return Task.CompletedTask;
        }
    }
}
