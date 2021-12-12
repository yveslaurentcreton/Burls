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
    public class ProfileSelectionRuleCreatedNotificationHandler : INotificationHandler<ProfileSelectionRuleCreatedNotification>
    {
        private readonly IBrowserStateNotificationService _browserStateNotificationService;

        public ProfileSelectionRuleCreatedNotificationHandler(IBrowserStateNotificationService browserStateNotificationService)
        {
            _browserStateNotificationService = browserStateNotificationService;
        }

        public Task Handle(ProfileSelectionRuleCreatedNotification notification, CancellationToken cancellationToken)
        {
            _browserStateNotificationService.RaiseSelectionRuleCreated(notification.SelectionRule);

            return Task.CompletedTask;
        }
    }
}
