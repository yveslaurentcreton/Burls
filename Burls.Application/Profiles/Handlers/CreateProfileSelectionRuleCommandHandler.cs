using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Data;
using Burls.Application.Profiles.Commands;
using Burls.Application.Profiles.Notifications;
using Burls.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Profiles.Handlers
{
    public class CreateProfileSelectionRuleCommandHandler : IRequestHandler<CreateProfileSelectionRuleCommand>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProfileSelectionRuleCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateProfileSelectionRuleCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.ProfileRepository.GetAsync(request.ProfileId);
            var selectionRule = new SelectionRule(request.SelectionRulePart, request.SelectionRuleCompareType, request.Value);

            profile.SelectionRules.Add(selectionRule);

            await _mediator.Publish(new ProfileSelectionRuleCreatedNotification(profile, selectionRule), cancellationToken);
            
            return Unit.Value;
        }
    }
}
