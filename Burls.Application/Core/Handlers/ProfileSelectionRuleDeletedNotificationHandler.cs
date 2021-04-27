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
    public class ProfileSelectionRuleDeletedNotificationHandler : INotificationHandler<ProfileSelectionRuleDeletedNotification>
    {
        private readonly IMediator _mediator;

        public ProfileSelectionRuleDeletedNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(ProfileSelectionRuleDeletedNotification notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new LoadBrowserStateBrowserProfilesCommand(notification.Profile.Id));
        }
    }
}
