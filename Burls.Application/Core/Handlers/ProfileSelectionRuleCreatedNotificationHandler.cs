using Burls.Application.Core.Commands;
using Burls.Application.Profiles.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Core.Handlers
{
    public class ProfileSelectionRuleCreatedNotificationHandler : INotificationHandler<ProfileSelectionRuleCreatedNotification>
    {
        private readonly IMediator _mediator;

        public ProfileSelectionRuleCreatedNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(ProfileSelectionRuleCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new LoadBrowserStateBrowserProfilesCommand(notification.Profile.Id));
        }
    }
}
