using Burls.Application.Core;
using Burls.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Burls.Domain.SelectionRule;

namespace Burls.Application.Profiles.Commands
{
    public class UpdateProfileSelectionRuleCommand : IRequest, ITransactionalRequest
    {
        public int ProfileId { get; }
        public int SelectionRuleId { get; }
        public SelectionRuleParts SelectionRulePart { get; }
        public SelectionRuleCompareTypes SelectionRuleCompareType { get; }
        public string Value { get; }

        public UpdateProfileSelectionRuleCommand(int profileId, int selectionRuleId, SelectionRuleParts selectionRulePart, SelectionRuleCompareTypes selectionRuleCompareType, string value)
        {
            ProfileId = profileId;
            SelectionRuleId = selectionRuleId;
            SelectionRulePart = selectionRulePart;
            SelectionRuleCompareType = selectionRuleCompareType;
            Value = value;
        }
    }
}
