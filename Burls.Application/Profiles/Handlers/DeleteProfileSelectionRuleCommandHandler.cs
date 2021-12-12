using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Data;
using Burls.Application.Profiles.Commands;
using Burls.Application.Profiles.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Profiles.Handlers
{
    public class DeleteProfileSelectionRuleCommandHandler : IRequestHandler<DeleteProfileSelectionRuleCommand>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProfileSelectionRuleCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProfileSelectionRuleCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetAsync(request.ProfileId);
            var selectionRule = profile.SelectionRules.Single(x => x.Id == request.SelectionRuleId);

            // Persist changes
            profile.SelectionRules.Remove(selectionRule);

            // Notify that everything is complete
            await _mediator.Publish(new ProfileSelectionRuleDeletedNotification(profile, selectionRule), cancellationToken);

            return Unit.Value;
        }
    }
}
