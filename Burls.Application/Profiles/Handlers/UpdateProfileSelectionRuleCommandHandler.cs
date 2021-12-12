using Burls.Application.Core.Data;
using Burls.Application.Profiles.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Profiles.Handlers
{
    public class UpdateProfileSelectionRuleCommandHandler : IRequestHandler<UpdateProfileSelectionRuleCommand>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileSelectionRuleCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProfileSelectionRuleCommand request, CancellationToken cancellationToken)
{
            // Get the record to update
            var profile = await _unitOfWork.ProfileRepository.GetAsync(request.ProfileId);
            var selectionRule = profile.SelectionRules.Single(x => x.Id == request.SelectionRuleId);

            // Update
            selectionRule.SelectionRulePart = request.SelectionRulePart;
            selectionRule.SelectionRuleCompareType = request.SelectionRuleCompareType;
            selectionRule.Value = request.Value;

            return Unit.Value;
        }
    }
}
